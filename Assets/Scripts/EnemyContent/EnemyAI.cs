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
        [SerializeField] private EnemyAnimation _enemyAnimation;
        [SerializeField] private EnemyHealthHandler _enemyHealthHandler;

        private NavMeshAgent _agent;
        private float _lastAttackTime;

        public IDamageable Player { get; private set; }

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            Player = _player.GetComponent<IDamageable>();
        }

        private void OnEnable()
        {
            _enemyHealthHandler.Died += StopNavMesh;
        }

        private void OnDisable()
        {
            _enemyHealthHandler.Died -= StopNavMesh;
        }

        private void Start()
        {
            Player = _player.GetComponent<IDamageable>();
        }

        private void Update()
        {
            if (_player == null || _enemyHealthHandler.IsDead) return;

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

        private void StopNavMesh()
        {
            _agent.isStopped = true;
            _agent.ResetPath();
        }

        private void TryAttack()
        {
            if (Time.time - _lastAttackTime < _attackCooldown) return;

            _lastAttackTime = Time.time;
            _enemyAnimation.PlayAttack();
        }

        public bool IsPlayerInAttackRange()
        {
            if (_player == null)
                return false;

            return Vector3.Distance(transform.position, _player.position) <= _attackRange;
        }
    }
}