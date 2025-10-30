using UnityEngine;

namespace EnemyContent.EnemyAttacks
{
    public class ZombieAttack : EnemyAttack
    {
        [SerializeField] private EnemyAI _enemyAI;

        private void OnEnable()
        {
            EnemyAnimation.AttackHitEvent += Attack;
        }

        private void OnDisable()
        {
            EnemyAnimation.AttackHitEvent -= Attack;
        }

        public override void Attack()
        {
            if (_enemyAI.IsPlayerInAttackRange())
                _enemyAI.Player.TakeDamage(Damage, Vector3.zero, _enemyAI.transform.position);
            else
                Debug.Log("Удар промахнулся — игрок слишком далеко");
        }
    }
}