using System;
using HealthContent;
using Interfaces;
using UnityEngine;

namespace PlayerContent
{
    public class PlayerLifeSystem : MonoBehaviour, IDamageable
    {
        [SerializeField] private Health _health;

        public event Action PlayerDied;
        
        private void OnEnable()
        {
            _health.Died += OnDie;
        }

        private void OnDisable()
        {
            _health.Died -= OnDie;
        }

        public void TakeDamage(int amount, Vector3 force, Vector3 hitPoint)
        {
            _health.DecreaseHealth(amount);
        }

        private void OnDie()
        {
            PlayerDied?.Invoke();
        }
    }
}