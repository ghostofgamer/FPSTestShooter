using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace SOContent.Waves
{
    [CreateAssetMenu(fileName = "WaveConfig", menuName = "Spawner/WaveSettings")]
    public class WaveConfig : ScriptableObject
    {
        [SerializeField] private List<EnemyEntry> _enemies = new List<EnemyEntry>();
        [SerializeField] private float _spawnInterval = 1f;
        [SerializeField] private float _startDelay = 0.5f;
        [SerializeField] private float _endDelay = 1f;

        public List<EnemyEntry> Enemies => _enemies;
        public float SpawnInterval => _spawnInterval;
        public float StartDelay => _startDelay;
        public float EndDelay => _endDelay;

        public List<EnemyType> GetShuffledEnemyList()
        {
            List<EnemyType> list = new List<EnemyType>();
            foreach (var entry in _enemies)
            {
                for (int i = 0; i < entry.Count; i++)
                {
                    list.Add(entry.Type);
                }
            }

            for (int i = 0; i < list.Count; i++)
            {
                int randomIndex = UnityEngine.Random.Range(i, list.Count);
                EnemyType temp = list[i];
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }

            return list;
        }
    }

    [Serializable]
    public class EnemyEntry
    {
        public EnemyType Type;
        public int Count = 1;
    }
}