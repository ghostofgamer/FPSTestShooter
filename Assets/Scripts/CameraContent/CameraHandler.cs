using System.Collections;
using PlayerContent;
using UnityEngine;

namespace CameraContent
{
    public class CameraHandler : MonoBehaviour
    {
// @formatter:off
        [Header("References")]
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField]private Camera _camera;
        [SerializeField]private GameObject _crosshair;
        [Space(3)]
        [Header("Settings")]
        [SerializeField] private float _defaultFOV = 60f;
        [SerializeField] private float _aimFOV = 35f;
        [SerializeField] private float _fovTransitionTime = 0.3f;
// @formatter:on

        private Coroutine _coroutine;
        private float _targetFov;
        private float _elapsedTime;
        private float _startFov;

        private void OnEnable()
        {
            _playerInput.AimStateChanged += Aiming;
        }

        private void OnDisable()
        {
            _playerInput.AimStateChanged -= Aiming;
        }

        private void Aiming(bool isAiming)
        {
            _targetFov = isAiming ? _aimFOV : _defaultFOV;
            _crosshair.SetActive(!isAiming);

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(ChangeFOV(_camera, _targetFov, _fovTransitionTime));
        }

        private IEnumerator ChangeFOV(Camera cam, float targetFOV, float duration)
        {
            _startFov = cam.fieldOfView;
            _elapsedTime = 0f;

            while (_elapsedTime < duration)
            {
                _elapsedTime += Time.deltaTime;
                cam.fieldOfView = Mathf.Lerp(_startFov, targetFOV, _elapsedTime / duration);
                yield return null;
            }

            cam.fieldOfView = targetFOV;
        }
    }
}