using System;
using Interfaces;
using UnityEngine;

namespace HealthContent
{
    public class Health : MonoBehaviour, IDamageable
    {
        private int _maxHealth = 100;
        private int _minHealth = 0;
        private bool _isDead = false;

        public event Action Died;
        public event Action<int, int> HealthChanged;

        public int CurrentHealth { get; private set; }

        private void Awake()
        {
            Init(100, 100);
        }

        public void Init(int currentHealth, int maxHealth)
        {
            CurrentHealth = currentHealth;
            _maxHealth = maxHealth;
            HealthChanged?.Invoke(CurrentHealth, _maxHealth);
        }

        public void IncreaseHealth(int amount)
        {
            CurrentHealth += amount;

            if (CurrentHealth > _maxHealth)
                CurrentHealth = _maxHealth;

            HealthChanged?.Invoke(CurrentHealth, _maxHealth);
        }

        private void DecreaseHealth(int damage)
        {
            if (_isDead)
                return;

            CurrentHealth -= damage;

            if (CurrentHealth <= _minHealth)
            {
                Died?.Invoke();
                _isDead = true;
                CurrentHealth = _minHealth;
            }

            HealthChanged?.Invoke(CurrentHealth, _maxHealth);
        }

        public void Reset()
        {
            _isDead = false;
            Init(100, 100);
        }

        public void TakeDamage(int amount, Vector3 force, Vector3 hitPoint)
        {
            Debug.Log("TakeDamage" + amount);
            DecreaseHealth(amount);
        }
    }
}