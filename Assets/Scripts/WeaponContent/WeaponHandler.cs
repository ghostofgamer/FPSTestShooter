using System.Collections;
using PlayerContent;
using UnityEngine;

namespace WeaponContent
{
    public class WeaponHandler : MonoBehaviour
    {
// @formatter:off
        [Header("References")]
        [SerializeField] private Weapon _currentWeapon;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private Transform _weaponPosDefault;
        [SerializeField] private Transform _weaponPosAiming;
        [Header("Settings")]
        [SerializeField]private float _aimTransitionTime = 0.3f;
// @formatter:on

        private Coroutine _aimCoroutine;

        private void OnEnable()
        {
            // _playerInput.MouseZeroKeyPressed+=
            _playerInput.RKeyPressed += Reload;
            _playerInput.AimStateChanged += Aiming;
        }

        private void OnDisable()
        {
            _playerInput.RKeyPressed -= Reload;
            _playerInput.AimStateChanged -= Aiming;
        }

        private void Shoot()
        {
        }

        private void Reload()
        {
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