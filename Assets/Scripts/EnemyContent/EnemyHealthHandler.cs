using HealthContent;
using UnityEngine;

namespace EnemyContent
{
    public class EnemyHealthHandler : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private EnemyAnimation _enemyAnimation;
        [SerializeField] private RagdollHandler _ragdollHandler;

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
            _enemyAnimation.DisableAnimator();
            _ragdollHandler.EnableRagdoll();
        }
    }
}