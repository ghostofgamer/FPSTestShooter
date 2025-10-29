using HealthContent;
using Interfaces;
using UnityEngine;

namespace EnemyContent.EnemyAttacks
{
    public class ClownAttack : EnemyAttack
    {
        [SerializeField]private Health _health;
        [SerializeField]private EnemyAnimation _enemyAnimation;
        [SerializeField] private float _radius = 5f;
        [SerializeField] private int _damage = 20;
        [SerializeField]private ParticleSystem _explodeParticles;
        [SerializeField]private AudioSource _audioSource;
        [SerializeField] private AudioClip _explosionClip;
        
        private bool _hasExploded;
        
        private void OnEnable()
        {
            _enemyAnimation.AttackHitEvent += Attack;
            _health.Died += Explosion;
            _hasExploded = false;
        }
        
        private void OnDisable()
        {
            _enemyAnimation.AttackHitEvent -= Attack;
            _health.Died -= Explosion;
        }
        
        public override void Attack()
        {
            Explosion();
        }

        private void Explosion()
        {
            if (_hasExploded)
                return;
            
            _hasExploded = true; 
            
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _radius);

            foreach (var collider in hitColliders)
            {
                if (collider.TryGetComponent<IDamageable>(out var damageable))
                {
                    Debug.Log("Урон нанес взрывом " + damageable.GetType().Name);

                    Vector3 force = (collider.transform.position - transform.position).normalized * 5f;
                    damageable.TakeDamage(_damage, force, transform.position);
                }
            }
            
            _explodeParticles.Play();
            _audioSource.PlayOneShot(_explosionClip);
            Debug.Log("взрыв");
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}