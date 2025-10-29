using System;
using System.Collections;
using SOContent.Waves;
using UnityEngine;

namespace WaveContent
{
    public class WaveSequence : MonoBehaviour
    {
        [SerializeField] private WaveConfig[] _waves;
        [SerializeField] private Spawner _spawner;

        private int _currentWaveIndex = 0;
        private Coroutine _waveCoroutine;

        public event Action<int, int> ProgressChanged;
        public event Action<float> OnCountdownTick;
        public event Action OnWaveStarted;

        public bool WaveSpawned;
        
        public void StartNextWave()
        {
            if (_waveCoroutine != null)
                StopCoroutine(_waveCoroutine);

            _waveCoroutine = StartCoroutine(RunWaves());
        }

        private IEnumerator RunWaves()
        {
            WaveSpawned = false;
            int spawned = 0;
            int totalEnemies = 0;

            var wave = _waves[_currentWaveIndex];

            foreach (var entry in wave.Enemies)
                totalEnemies += entry.Count;

            ProgressChanged?.Invoke(spawned, totalEnemies);

            float timer = wave.StartDelay;
            
            while (timer > 0)
            {
                OnCountdownTick?.Invoke(timer);
                yield return new WaitForSeconds(1f);
                timer -= 1f;
            }
            
            OnWaveStarted?.Invoke();
            
            foreach (var entry in wave.Enemies)
            {
                for (int i = 0; i < entry.Count; i++)
                {
                    _spawner.SpawnEnemy(entry.Type);
                    spawned++;
                    ProgressChanged?.Invoke(spawned, totalEnemies);

                    yield return new WaitForSeconds(wave.SpawnInterval);
                }
            }

            WaveSpawned = true;
            
            _currentWaveIndex++;

            if (_currentWaveIndex >= _waves.Length)
                Debug.Log("Все волны завершены!");
        }
    }
}