using UnityEngine;

namespace SOContent
{
    [CreateAssetMenu(fileName = "NewWeaponConfig", menuName = "Weapons/WeaponConfig")]
    public class WeaponConfig : ScriptableObject
    {
// @formatter:off
        [Header("Stats")]
        [SerializeField] private float _range;
        [SerializeField] private int _damage;
        [SerializeField] private float _fireRate;      
        [SerializeField] private float _force;      

        [Header("Effects")]
        [SerializeField] private ParticleSystem _muzzleFlash;
        [SerializeField] private AudioClip _fireSound;
        [SerializeField] private AnimatorOverrideController _weaponAnimator;

        public float Range => _range;
        public int Damage => _damage;
        public float Force => _force;
        public float FireRate => _fireRate;
        public ParticleSystem MuzzleFlash => _muzzleFlash;
        public AudioClip FireSound => _fireSound;
        public AnimatorOverrideController WeaponAnimator => _weaponAnimator;
// @formatter:on
    }
}