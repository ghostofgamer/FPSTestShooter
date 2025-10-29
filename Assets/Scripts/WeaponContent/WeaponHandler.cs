using System.Collections;
using PlayerContent;
using UnityEngine;

namespace WeaponContent
{
    public class WeaponHandler : MonoBehaviour
    {
// @formatter:off
        [Header("References")]
        [SerializeField]private Camera _camera;
        [SerializeField] private Weapon _currentWeapon;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private Transform _weaponPosDefault;
        [SerializeField] private Transform _weaponPosAiming;
        [SerializeField] private LayerMask _ignoreMask;
        [Header("Settings")]
        [SerializeField]private float _aimTransitionTime = 0.3f;
// @formatter:on

        private Coroutine _aimCoroutine;
        private Coroutine _autoFireCoroutine;
        private bool _isShooting = false;

        private void OnEnable()
        {
            _playerInput.RKeyPressed += Reload;
            _playerInput.AimStateChanged += Aiming;
            _playerInput.MouseLeftKeyPressed += Shoot;
            _playerInput.MouseLeftKeyReleased += StopShoot;
        }

        private void OnDisable()
        {
            _playerInput.RKeyPressed -= Reload;
            _playerInput.AimStateChanged -= Aiming;
            _playerInput.MouseLeftKeyPressed -= Shoot;
            _playerInput.MouseLeftKeyReleased -= StopShoot;
        }

        private void Shoot()
        {
            if (_currentWeapon == null)
                return;

            _isShooting = true;

            if (_autoFireCoroutine != null)
                StopCoroutine(_autoFireCoroutine);

            _autoFireCoroutine = StartCoroutine(AutoFire());
        }

        private void StopShoot()
        {
            _isShooting = false;

            if (_autoFireCoroutine != null)
            {
                StopCoroutine(_autoFireCoroutine);
                _autoFireCoroutine = null;
            }
        }

        private IEnumerator AutoFire()
        {
            while (_isShooting)
            {
                FireWeapon();
                yield return new WaitForSeconds(0.1f);
            }
        }

        private void FireWeapon()
        {
            if (_currentWeapon == null || _currentWeapon.IsReload)
                return;

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            _currentWeapon.Shoot();

            float range = _currentWeapon.WeaponConfig.Range;
            int layerMaskForRaycast = ~_ignoreMask;

            if (Physics.Raycast(ray, out RaycastHit hit, range, layerMaskForRaycast, QueryTriggerInteraction.Ignore))
                _currentWeapon.OnHit(hit);
        }

        private void Reload()
        {
            if (_currentWeapon != null && !_currentWeapon.IsReload)
                _currentWeapon.WeaponReload();
        }

        private void Aiming(bool isAiming)
        {
            if (_aimCoroutine != null)
                StopCoroutine(_aimCoroutine);

            _aimCoroutine = StartCoroutine(MoveWeapon(isAiming));
        }

        private IEnumerator MoveWeapon(bool toAiming)
        {
            Transform targetPos = toAiming ? _weaponPosAiming : _weaponPosDefault;

            Vector3 startPos = _currentWeapon.transform.localPosition;
            Quaternion startRot = _currentWeapon.transform.localRotation;

            Vector3 endPos = targetPos.localPosition;
            Quaternion endRot = targetPos.localRotation;

            float elapsed = 0f;

            while (elapsed < _aimTransitionTime)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / _aimTransitionTime;
                _currentWeapon.transform.localPosition = Vector3.Lerp(startPos, endPos, t);
                _currentWeapon.transform.localRotation = Quaternion.Lerp(startRot, endRot, t);
                yield return null;
            }

            _currentWeapon.transform.localPosition = endPos;
            _currentWeapon.transform.localRotation = endRot;
        }
    }
}