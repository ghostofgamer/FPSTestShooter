using System;
using System.Collections.Generic;
using EnemyContent;
using UnityEngine;

namespace WaveContent
{
    public class EnemyWaveCounter : MonoBehaviour
    {
        [SerializeField] private WaveSequence _waveSequence;

        private List<EnemyAI> _enemies = new List<EnemyAI>();

        public event Action WaveCleared;

        private void OnEnable()
        {
            _waveSequence.OnWaveStarted += SetupWave;
        }

        private void OnDisable()
        {
            _waveSequence.OnWaveStarted -= SetupWave;
        }

        private void SetupWave()
        {
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

            if (_waveSequence.WaveSpawned && _enemies.Count <= 0)
            {
                WaveCleared?.Invoke();
                Debug.Log("Убил всех молодец");
            }
        }

        private void Clear()
        {
            _enemies.Clear();
        }
    }
}