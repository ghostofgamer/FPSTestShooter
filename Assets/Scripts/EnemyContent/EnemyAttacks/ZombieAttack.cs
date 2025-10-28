using UnityEngine;

namespace EnemyContent.EnemyAttacks
{
    public class ZombieAttack : EnemyAttack
    {
        [SerializeField]private EnemyAnimation _enemyAnimation;
        [SerializeField]private EnemyAI _enemyAI;
        
        private void OnEnable()
        {
            _enemyAnimation.AttackHitEvent += Attack;
        }
        
        private void OnDisable()
        {
            _enemyAnimation.AttackHitEvent -= Attack;
        }
        
        public override void Attack()
        {
            if (_enemyAI.IsPlayerInAttackRange())
            {
                Debug.Log("Удар ");
                _enemyAI.Player.TakeDamage(10, Vector3.zero, _enemyAI.transform.position);
            }
            else
            {
                Debug.Log("Удар промахнулся — игрок слишком далеко");
            }
        }
    }
}