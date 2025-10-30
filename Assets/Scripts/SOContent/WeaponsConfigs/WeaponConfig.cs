using UnityEngine;

namespace SOContent.WeaponsConfigs
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
        [SerializeField]private int _ammoMagazine;

        [Header("Effects")]
        [SerializeField] private AudioClip _fireSound;
        [SerializeField] private AudioClip _blankShotSound;
        [SerializeField] private AudioClip _reloadSound;

        public float Range => _range;
        public int Damage => _damage;
        public float Force => _force;
        public float FireRate => _fireRate;
        public int AmmoMagazine => _ammoMagazine;
        public AudioClip FireSound => _fireSound;
        public AudioClip BlankShotSound => _blankShotSound;
        public AudioClip ReloadSound => _reloadSound;
// @formatter:on
    }
}