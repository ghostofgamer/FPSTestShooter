using System;
using Interfaces;
using UnityEngine;

namespace HealthContent
{
    public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _maxHealth = 100;
        [SerializeField] private int _minHealth = 0;

        public event Action Died;
        public event Action<int, int> HealthChanged;

        public bool IsDead { get; private set; } = false;
        public int CurrentHealth { get; private set; }

        private void Init(int currentHealth, int maxHealth)
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
            if (IsDead)
                return;

            CurrentHealth -= damage;
            
            Debug.Log("Damaged " + gameObject.name + " to " + damage);
            Debug.Log("CurrentHealth " + gameObject.name + " to " + CurrentHealth);
            Debug.Log("_minHealth " + gameObject.name + " to " + _minHealth);

            if (CurrentHealth <= _minHealth)
            {
                Died?.Invoke();
                IsDead = true;
                CurrentHealth = _minHealth;
            }

            HealthChanged?.Invoke(CurrentHealth, _maxHealth);
        }

        public void Reset()
        {
            IsDead = false;
            Init(_maxHealth, _maxHealth);
        }

        public void TakeDamage(int amount, Vector3 force, Vector3 hitPoint)
        {
            DecreaseHealth(amount);
        }
    }
}