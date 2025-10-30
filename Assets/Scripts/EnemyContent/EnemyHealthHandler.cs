using System;
using System.Collections;
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

        private Coroutine _coroutine;

        public bool IsDead => _health.CurrentHealth <= 0;

        public event Action<EnemyAI> Died;

        private void Awake()
        {
            _health.Reset();
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

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            StartCoroutine(DeactivateAfterDelay(5f));
        }

        private IEnumerator DeactivateAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            _health.Reset();
            _ragdollHandler.DisableRagdoll();
            _enemyAnimation.EnableAnimator();
            gameObject.SetActive(false);
        }
    }
}