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
        private float _elapsedTime;
        private float _time;
        private float _range;
        private int _layerMaskForRaycast;
        private Vector3 _startPosition;
        private Vector3 _endPosition;
        private Quaternion _startRotation;
        private Quaternion _endRotation;
        private Transform _targetPos;
        private Ray _ray;

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
                yield return new WaitForSeconds(_currentWeapon.WeaponConfig.FireRate);
            }
        }

        private void FireWeapon()
        {
            if (_currentWeapon == null || _currentWeapon.IsReload)
                return;

            _ray = _camera.ScreenPointToRay(Input.mousePosition);
            _currentWeapon.Shoot();

            _range = _currentWeapon.WeaponConfig.Range;
            _layerMaskForRaycast = ~_ignoreMask;

            if (Physics.Raycast(_ray, out RaycastHit hit, _range, _layerMaskForRaycast, QueryTriggerInteraction.Ignore))
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
            _targetPos = toAiming ? _weaponPosAiming : _weaponPosDefault;
            _startPosition = _currentWeapon.transform.localPosition;
            _startRotation = _currentWeapon.transform.localRotation;
            _endPosition = _targetPos.localPosition;
            _endRotation = _targetPos.localRotation;
            _elapsedTime = 0f;

            while (_elapsedTime < _aimTransitionTime)
            {
                _elapsedTime += Time.deltaTime;
                _time = _elapsedTime / _aimTransitionTime;
                _currentWeapon.transform.localPosition = Vector3.Lerp(_startPosition, _endPosition, _time);
                _currentWeapon.transform.localRotation = Quaternion.Lerp(_startRotation, _endRotation, _time);
                yield return null;
            }

            _currentWeapon.transform.localPosition = _endPosition;
            _currentWeapon.transform.localRotation = _endRotation;
        }
    }
}