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
    }

    [Serializable]
    public class EnemyEntry
    {
        public EnemyType Type;
        public int Count = 1;
    }
}