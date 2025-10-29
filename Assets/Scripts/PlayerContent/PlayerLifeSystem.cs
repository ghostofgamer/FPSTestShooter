using System;
using HealthContent;
using UnityEngine;

namespace PlayerContent
{
    public class PlayerLifeSystem : MonoBehaviour
    {
        [SerializeField] private Health _health;

        public Health Health=>_health;
        
        public event Action PlayerDied;
        
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
            PlayerDied?.Invoke();
        }
    }
}