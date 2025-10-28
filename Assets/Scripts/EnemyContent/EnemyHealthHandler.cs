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

        public bool IsDead => _health.CurrentHealth <= 0;

        public event Action Died;
        
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
            Died?.Invoke();
            _enemyAnimation.DisableAnimator();
            _ragdollHandler.EnableRagdoll();
        }
    }
}