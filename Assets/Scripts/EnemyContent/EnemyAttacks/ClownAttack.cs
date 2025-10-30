using HealthContent;
using Interfaces;
using UnityEngine;

namespace EnemyContent.EnemyAttacks
{
    public class ClownAttack : EnemyAttack
    {
        [SerializeField] private Health _health;
        [SerializeField] private float _radius = 5f;
        [SerializeField] private float _forceFactor = 5f;
        [SerializeField] private ParticleSystem _explodeParticles;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _explosionClip;

        private bool _hasExploded;
        private Vector3 _force;

        private void OnEnable()
        {
            EnemyAnimation.AttackHitEvent += Attack;
            _health.Died += Explosion;
            _hasExploded = false;
        }

        private void OnDisable()
        {
            EnemyAnimation.AttackHitEvent -= Attack;
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
                    _force = (collider.transform.position - transform.position).normalized * _forceFactor;
                    damageable.TakeDamage(Damage, _force, transform.position);
                }
            }

            _explodeParticles.Play();
            _audioSource.PlayOneShot(_explosionClip);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}