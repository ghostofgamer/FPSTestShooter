using System;
using UnityEngine;

namespace HealthContent
{
    public class Health : MonoBehaviour
    {
        private int _maxHealth = 100;
        private int _minHealth = 0;
        private bool _isDead = false;

        public event Action Died;
        
        public int CurrentHealth { get; private set; }

        private void Awake()
        {
            Init(100, 100);
        }

        public void Init(int currentHealth, int maxHealth)
        {
            CurrentHealth = currentHealth;
            _maxHealth = maxHealth;
        }

        public void IncreaseHealth(int amount)
        {
            CurrentHealth += amount;

            if (CurrentHealth > _maxHealth)
                CurrentHealth = _maxHealth;
        }

        public void DecreaseHealth(int damage)
        {
            if (_isDead)
                return;

            CurrentHealth -= damage;

            if (CurrentHealth < _minHealth)
            {
                Died?.Invoke();
                _isDead = true;
                CurrentHealth = _minHealth;
            }
        }
    }
}