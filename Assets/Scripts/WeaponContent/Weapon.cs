using SOContent;
using UnityEngine;

namespace WeaponContent
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponConfig _weaponConfig;
        [SerializeField] private ParticleSystem _muzzleEffect;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private WeaponAnimator _weaponAnimator;

        public WeaponConfig WeaponConfig => _weaponConfig;

        public void OnHit(RaycastHit hit)
        {
            Debug.Log("hit " + hit.collider.gameObject.name);
        }

        public void Shoot()
        {
            _muzzleEffect.Play();
            _audioSource.PlayOneShot(_weaponConfig.FireSound);
            _weaponAnimator.PlayWeaponAnimation();
        }
    }
}