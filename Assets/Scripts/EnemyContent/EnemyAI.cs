using EnemyContent.EnemyAttacks;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyContent
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyAI : MonoBehaviour
    {
// @formatter:off
        [Header("References")]
        [SerializeField] private EnemyAttack _enemyAttack;
        [SerializeField] private EnemyAnimation _enemyAnimation;
        [SerializeField] private EnemyHealthHandler _enemyHealthHandler;
        [Header("Settings")]
        [SerializeField] private float _attackRange = 2f;
        [SerializeField] private float _attackCooldown = 1f;
// @formatter:on

        private Transform _player;
        private NavMeshAgent _agent;
        private float _lastAttackTime;
        private float _distance;

        public EnemyHealthHandler EnemyHealthHandler => _enemyHealthHandler;
        public IDamageable Player { get; private set; }

        private void OnEnable()
        {
            _enemyHealthHandler.Died += StopNavMesh;
        }

        private void OnDisable()
        {
            _enemyHealthHandler.Died -= StopNavMesh;
        }

        private void Update()
        {
            if (_player == null || _enemyHealthHandler.IsDead) return;

            _distance = Vector3.Distance(transform.position, _player.position);

            if (_distance > _attackRange)
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

        public void Init(Transform player)
        {
            _enemyHealthHandler.Init();
            _player = player;
            _agent = GetComponent<NavMeshAgent>();
            this.Player = _player.GetComponent<IDamageable>();
        }

        public bool IsPlayerInAttackRange()
        {
            if (_player == null)
                return false;

            return Vector3.Distance(transform.position, _player.position) <= _attackRange;
        }

        private void StopNavMesh(EnemyAI enemyAI)
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
    }
}