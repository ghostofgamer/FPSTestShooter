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
// @formatter:off
        [Header("References")]
        [SerializeField] private WaveConfig _baseWave;
        [SerializeField] private Spawner _spawner;
        [Header("Settings")]
        [SerializeField] private float _growthFactor = 1.5f;
        [SerializeField] private float _spawnSpeedup = 0.95f;
        [SerializeField] private int _maxEnemiesCap = 500;
// @formatter:on

        private Coroutine _waveCoroutine;
        private WaitForSeconds _waitForSecond = new WaitForSeconds(1f);
        private WaitForSeconds _waitForSecondWave;
        private int _spawned;
        private int _totalEnemies;
        private float _timer;
        private float _countdownTick = 1f;
        private float _minSpawnInterval = 0.2f;

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
            WaveSpawned = false;
            WaveConfig wave = GenerateScaledWave(CurrentWaveIndex);
            _spawned = 0;
            _totalEnemies = 0;
            List<EnemyType> shuffledEnemies = wave.GetShuffledEnemyList();
            _totalEnemies = shuffledEnemies.Count;
            ProgressChanged?.Invoke(_spawned, _totalEnemies);
            _timer = wave.StartDelay;

            while (_timer > 0)
            {
                OnCountdownTick?.Invoke(_timer);
                yield return _waitForSecond;
                _timer -= _countdownTick;
            }

            OnWaveStarted?.Invoke(_totalEnemies);
            _waitForSecondWave = new WaitForSeconds(wave.SpawnInterval);

            foreach (var enemyType in shuffledEnemies)
            {
                _spawner.SpawnEnemy(enemyType);
                _spawned++;

                if (_spawned >= _totalEnemies)
                    WaveSpawned = true;

                ProgressChanged?.Invoke(_spawned, _totalEnemies);
                yield return _waitForSecondWave;
            }

            CurrentWaveIndex++;
        }

        private WaveConfig GenerateScaledWave(int waveIndex)
        {
            WaveConfig newWave = ScriptableObject.CreateInstance<WaveConfig>();
            var enemyList = new List<EnemyEntry>();

            foreach (var e in _baseWave.Enemies)
            {
                int scaledCount = Mathf.Min(Mathf.CeilToInt(e.Count * Mathf.Pow(_growthFactor, waveIndex)),
                    _maxEnemiesCap);
                enemyList.Add(new EnemyEntry { Type = e.Type, Count = scaledCount });
            }

            float scaledSpawnInterval = Mathf.Max(_baseWave.SpawnInterval * Mathf.Pow(_spawnSpeedup, waveIndex),
                _minSpawnInterval);

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