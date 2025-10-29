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
    [SerializeField] private EnemyWaveCounter _enemyWaveCounter;

    private int _currentWaveIndex = 0;

    public void SpawnEnemy(EnemyType type)
    {
        var enemy = _enemyPools.GetEnemy(type);

        if (enemy == null)
            return;

        Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        enemy.transform.position = spawnPoint.position;
        enemy.transform.rotation = spawnPoint.rotation;

        if (enemy.TryGetComponent(out EnemyAI enemyAI))
        {
            enemyAI.Init(_player.transform);

            if (_enemyWaveCounter != null)
                _enemyWaveCounter.AddEnemy(enemyAI);
        }
    }
}