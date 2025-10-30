using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using SOContent.Waves;
using UnityEngine;

namespace WaveContent
{
    public class WaveSequence : MonoBehaviour
    {
        [SerializeField] private WaveConfig[] _waves;
        [SerializeField] private WaveConfig _baseWave;
        [SerializeField] private Spawner _spawner;
        [SerializeField] private float _growthFactor = 1.5f;
        [SerializeField] private float _spawnSpeedup = 0.95f;
        [SerializeField] private int _maxEnemiesCap = 500;

        private Coroutine _waveCoroutine;

        public event Action<int, int> ProgressChanged;
        public event Action<float> OnCountdownTick;
        public event Action<int> OnWaveStarted;
        public event Action AllWavesCompleted;

        public int CurrentWaveIndex { get; private set; }
        public bool WaveSpawned { get; private set; }

        public void StartNextWave()
        {
            if (_waveCoroutine != null)
                StopCoroutine(_waveCoroutine);

            _waveCoroutine = StartCoroutine(RunWaves());
        }

        private IEnumerator RunWaves()
        {
            Debug.Log($"Старт волны #{CurrentWaveIndex + 1}");
          
            WaveSpawned = false;
            
            WaveConfig wave = GenerateScaledWave(CurrentWaveIndex); 
            
            int spawned = 0;
            int totalEnemies = 0;
            List<EnemyType> shuffledEnemies = wave.GetShuffledEnemyList();
            totalEnemies = shuffledEnemies.Count;
            ProgressChanged?.Invoke(spawned, totalEnemies);
            float timer = wave.StartDelay;

            while (timer > 0)
            {
                OnCountdownTick?.Invoke(timer);
                yield return new WaitForSeconds(1f);
                timer -= 1f;
            }

            OnWaveStarted?.Invoke(totalEnemies);

            foreach (var enemyType in shuffledEnemies)
            {
                _spawner.SpawnEnemy(enemyType);
                spawned++;

                if (spawned >= totalEnemies)
                    WaveSpawned = true;

                ProgressChanged?.Invoke(spawned, totalEnemies);
                yield return new WaitForSeconds(wave.SpawnInterval);
            }
            
            CurrentWaveIndex++;
        }
        
        private WaveConfig GenerateScaledWave(int waveIndex)
        {
            WaveConfig newWave = ScriptableObject.CreateInstance<WaveConfig>();
            
            var enemyList = new List<EnemyEntry>();
            
            foreach (var e in _baseWave.Enemies)
            {
                int scaledCount = Mathf.Min(
                    Mathf.CeilToInt(e.Count * Mathf.Pow(_growthFactor, waveIndex)),
                    _maxEnemiesCap
                );

                enemyList.Add(new EnemyEntry
                {
                    Type = e.Type,
                    Count = scaledCount
                });
            }
            
            float scaledSpawnInterval = Mathf.Max(
                _baseWave.SpawnInterval * Mathf.Pow(_spawnSpeedup, waveIndex),
                0.2f
            );
            
            newWave.Initialize(
                enemyList,
                scaledSpawnInterval,
                _baseWave.StartDelay,
                _baseWave.EndDelay
            );

            return newWave;
        }
    }
}