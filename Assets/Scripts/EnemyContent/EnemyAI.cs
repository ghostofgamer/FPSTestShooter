using EnemyContent.EnemyAttacks;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyContent
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private float _attackRange = 2f;
        [SerializeField] private float _attackCooldown = 1f;
        [SerializeField] private int _damage = 10;
        [SerializeField] private EnemyAttack _enemyAttack;

        private NavMeshAgent _agent;
        private float _lastAttackTime;
        private EnemyAnimation _enemyAnimation;
        private IDamageable _playerHealth;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _enemyAnimation = GetComponent<EnemyAnimation>();
            _playerHealth = _player.GetComponent<IDamageable>();
        }

        private void Update()
        {
            if (_player == null) return;

            float distance = Vector3.Distance(transform.position, _player.position);

            if (distance > _attackRange)
            {
                _agent.isStopped = false;
                _agent.SetDestination(_player.position);
                _enemyAnimation.PlayWalk(true);
            }
            else
            {
                _agent.isStopped = true;
                _enemyAnimation.PlayWalk(false);
                TryAttack();
            }
        }

        private void TryAttack()
        {
            if (Time.time - _lastAttackTime < _attackCooldown) return;

            _lastAttackTime = Time.time;
            _enemyAttack.Attack();
            
            _playerHealth?.TakeDamage(_damage, Vector3.zero, _player.position);
        }
    }
}