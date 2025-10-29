using System;
using HealthContent;
using UnityEngine;

namespace EnemyContent
{
    public class EnemyHealthHandler : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private EnemyAnimation _enemyAnimation;
        [SerializeField] private RagdollHandler _ragdollHandler;
        [SerializeField] private EnemyAI _enemyAI;

        public bool IsDead => _health.CurrentHealth <= 0;

        public event Action<EnemyAI> Died;

        private void Awake()
        {
            _ragdollHandler.Initialize();
        }

        private void OnEnable()
        {
            _health.Died += OnDie;
        }

        private void OnDisable()
        {
            _health.Died -= OnDie;
        }

        private void OnDie()
        {
            Died?.Invoke(_enemyAI);
            _enemyAnimation.DisableAnimator();
            _ragdollHandler.EnableRagdoll();
        }
    }
}