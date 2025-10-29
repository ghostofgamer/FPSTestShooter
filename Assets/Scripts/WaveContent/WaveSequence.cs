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

        private Coroutine _waveCoroutine;

        public event Action<int, int> ProgressChanged;
        public event Action<float> OnCountdownTick;
        public event Action<int> OnWaveStarted;
        public event Action AllWavesCompleted;

        public int CurrentWaveIndex{ get; private set; }
        public bool WaveSpawned { get; private set; }
        
        public void StartNextWave()
        {
            if (CurrentWaveIndex >= _waves.Length)
            {
                AllWavesCompleted?.Invoke();
                Debug.Log("Все волны завершены!");
                return;
            }
            
            if (_waveCoroutine != null)
                StopCoroutine(_waveCoroutine);

            _waveCoroutine = StartCoroutine(RunWaves());
        }

        private IEnumerator RunWaves()
        {
            WaveSpawned = false;
            int spawned = 0;
            int totalEnemies = 0;

            var wave = _waves[CurrentWaveIndex];

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
            
            OnWaveStarted?.Invoke(totalEnemies);
            
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
            CurrentWaveIndex++;
        }
    }
}