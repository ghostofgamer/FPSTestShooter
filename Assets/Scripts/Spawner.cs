using EnemyContent;
using Enums;
using ObjectPoolContent;
using PlayerContent;
using UnityEngine;
using WaveContent;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private EnemyPools _enemyPools;
    [SerializeField] private Player _player;
    [SerializeField] private WaveMonitor waveMonitor;

    private int _currentWaveIndex = 0;
    private Transform _spawnPoint;

    public void SpawnEnemy(EnemyType type)
    {
        var enemy = _enemyPools.GetEnemy(type);

        if (enemy == null)
            return;

        _spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        enemy.transform.position = _spawnPoint.position;
        enemy.transform.rotation = _spawnPoint.rotation;

        if (enemy.TryGetComponent(out EnemyAI enemyAI))
        {
            enemyAI.Init(_player.transform);

            if (waveMonitor != null)
                waveMonitor.AddEnemy(enemyAI);
        }
    }
}