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
        [SerializeField] private float _deactivateDelay;

        private Coroutine _coroutine;
        private WaitForSeconds _waitForSeconds;

        public event Action<EnemyAI> Died;

        public bool IsDead => _health.CurrentHealth <= 0;

        private void OnEnable()
        {
            _health.Died += OnDie;
        }

        private void OnDisable()
        {
            _health.Died -= OnDie;
        }

        public void Init()
        {
            _waitForSeconds = new WaitForSeconds(_deactivateDelay);
            _health.Reset();
            _ragdollHandler.Initialize();
        }

        private void OnDie()
        {
            Died?.Invoke(_enemyAI);
            _enemyAnimation.DisableAnimator();
            _ragdollHandler.EnableRagdoll();

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            StartCoroutine(DeactivateAfterDelay());
        }

        private IEnumerator DeactivateAfterDelay()
        {
            yield return _waitForSeconds;
            _health.Reset();
            _ragdollHandler.DisableRagdoll();
            _enemyAnimation.EnableAnimator();
            gameObject.SetActive(false);
        }
    }
}