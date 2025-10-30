using UnityEngine;

namespace EnemyContent.EnemyAttacks
{
    public abstract class EnemyAttack : MonoBehaviour
    {
        [SerializeField]private EnemyAnimation _enemyAnimation;
        [SerializeField] private int _damage = 20;
        
        protected EnemyAnimation EnemyAnimation => _enemyAnimation;
        protected int Damage => _damage; 
        
        public abstract void Attack();
    }
}