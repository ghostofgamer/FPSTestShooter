using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using UnityEngine;

namespace ObjectPoolContent
{
    public class EnemyPools : MonoBehaviour
    {
        [SerializeField] private List<EnemyInfo> _enemyList = new List<EnemyInfo>();

        private Dictionary<EnemyType, ObjectPool<MonoBehaviour>> _pools;

        public void Init()
        {
            CreatePools();
        }

        private void CreatePools()
        {
            _pools = new Dictionary<EnemyType, ObjectPool<MonoBehaviour>>();

            foreach (var enemy in _enemyList)
            {
                var pool = new ObjectPool<MonoBehaviour>(enemy.Prefab, enemy.InitialCount, transform);
                pool.EnableAutoExpand();
                _pools.Add(enemy.EnemyType, pool);
            }
        }

        public MonoBehaviour GetEnemy(EnemyType type)
        {
            if (!_pools.TryGetValue(type, out var pool))
            {
                Debug.LogWarning($"[EnemyPools] Нет пула для {type}");
                return null;
            }

            if (pool.TryGetObject(out var enemy, _enemyList.First(e => e.EnemyType == type).Prefab))
            {
                enemy.gameObject.SetActive(true);
                return enemy;
            }

            return null;
        }
    }

    [Serializable]
    public class EnemyInfo
    {
        public MonoBehaviour Prefab;
        public int InitialCount = 10;
        public EnemyType EnemyType = EnemyType.Empty;
    }
}