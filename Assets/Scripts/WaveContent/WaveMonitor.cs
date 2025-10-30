using System;
using System.Collections.Generic;
using EnemyContent;
using UnityEngine;

namespace WaveContent
{
    public class WaveMonitor : MonoBehaviour
    {
        [SerializeField] private WaveSequence _waveSequence;

        private List<EnemyAI> _enemies = new List<EnemyAI>();
        private int _currentEnemyAmount;

        public event Action WaveCleared;
        public event Action<int> EnemyAmountChanged;

        private void OnEnable()
        {
            _waveSequence.OnWaveStarted += SetupWave;
        }

        private void OnDisable()
        {
            _waveSequence.OnWaveStarted -= SetupWave;
        }

        private void SetupWave(int amount)
        {
            _currentEnemyAmount = amount;
            Clear();
        }

        public void AddEnemy(EnemyAI enemyAI)
        {
            if (enemyAI == null)
                return;

            _enemies.Add(enemyAI);
            enemyAI.EnemyHealthHandler.Died += DeleteEnemy;
        }

        private void DeleteEnemy(EnemyAI enemyAI)
        {
            if (enemyAI == null)
                return;

            enemyAI.EnemyHealthHandler.Died -= DeleteEnemy;
            _enemies.Remove(enemyAI);
            _currentEnemyAmount--;
            EnemyAmountChanged?.Invoke(_currentEnemyAmount);

            if (_waveSequence.WaveSpawned && _enemies.Count <= 0)
                WaveCleared?.Invoke();
        }

        private void Clear()
        {
            _enemies.Clear();
        }
    }
}