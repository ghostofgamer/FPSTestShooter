using System;
using System.Collections;
using SOContent.WeaponsConfigs;
using UnityEngine;

namespace WeaponContent
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponConfig _weaponConfig;
        [SerializeField] private ParticleSystem _muzzleEffect;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private WeaponAnimator _weaponAnimator;
        [SerializeField] private HitHandler _hitHandler;

        private int _currentAmmoMagazine;
        private WaitForSeconds _waitForReload = new WaitForSeconds(2f);
        private Coroutine _reloadCoroutine;

        public event Action<int> AmmoChanged;

        public bool IsReload { get; private set; }
        public WeaponConfig WeaponConfig => _weaponConfig;

        private void Start()
        {
            InitAmmo();
        }

        public void OnHit(RaycastHit hit)
        {
            if (!HasAmmo()) return;

            _hitHandler.ProcessHit(hit, _weaponConfig.Damage, _weaponConfig.Force);
        }

        public void Shoot()
        {
            if (!HasAmmo()) return;

            _currentAmmoMagazine--;
            AmmoChanged(_currentAmmoMagazine);
            _muzzleEffect.Play();
            _audioSource.PlayOneShot(_weaponConfig.FireSound);
            _weaponAnimator.PlayWeaponAnimation();
        }

        public void WeaponReload()
        {
            if (_reloadCoroutine != null)
                StopCoroutine(_reloadCoroutine);

            _reloadCoroutine = StartCoroutine(ReloadRoutine());
        }

        private void InitAmmo()
        {
            _currentAmmoMagazine = _weaponConfig.AmmoMagazine;
            AmmoChanged(_currentAmmoMagazine);
        }

        private IEnumerator ReloadRoutine()
        {
            IsReload = true;
            _weaponAnimator.PlayReload();
            _audioSource.PlayOneShot(_weaponConfig.ReloadSound);
            yield return _waitForReload;
            InitAmmo();
            IsReload = false;
        }

        private void BlankShot()
        {
            _audioSource.PlayOneShot(_weaponConfig.BlankShotSound);
        }

        private bool HasAmmo()
        {
            if (_currentAmmoMagazine <= 0)
            {
                BlankShot();
                return false;
            }

            return true;
        }
    }
}