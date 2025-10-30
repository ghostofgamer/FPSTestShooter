using PlayerContent;
using UnityEngine;

namespace WeaponContent
{
    public class WeaponSway : MonoBehaviour
    {
// @formatter:off
        [Header("References")]
        [SerializeField] private PlayerInput _playerInput; 
        [SerializeField]private WeaponAnimator _weaponAnimator;
        [Header("Sway Settings")]
        [SerializeField] private float _swayAmount = 0.05f;      
        [SerializeField] private float _maxSwayAmount = 0.1f;     
        [SerializeField] private float _smoothAmount = 8f;
        [SerializeField] private float _xSwayMultiplier = 20f; 
        [SerializeField] private float _ySwayMultiplier = 10f; 
        [SerializeField] private float _zSwayMultiplier = 15f;
        [SerializeField] private float _mouseMoveThreshold = 0.01f;
        [SerializeField]private float _factor = 0.5f;
// @formatter:on

        private Vector3 _initialPosition;
        private Quaternion _initialRotation;
        private float _mouseSpeed;
        private float _swayX;
        private float _swayY;
        private Vector3 _targetPosition;
        private Quaternion _targetRotation;

        private void Start()
        {
            _initialPosition = transform.localPosition;
            _initialRotation = transform.localRotation;
        }

        private void Update()
        {
            _mouseSpeed = Mathf.Clamp01(
                (Mathf.Abs(_playerInput.MouseXValue) + Mathf.Abs(_playerInput.MouseYValue)) * _factor);
            _weaponAnimator.PlayMoveIdleAnimation(_mouseSpeed);

            if (_playerInput.IsAiming)
                return;
            
            _swayX = Mathf.Clamp(-_playerInput.MouseXValue * _swayAmount, -_maxSwayAmount, _maxSwayAmount);
            _swayY = Mathf.Clamp(-_playerInput.MouseYValue * _swayAmount, -_maxSwayAmount, _maxSwayAmount);
            _targetPosition = _initialPosition + new Vector3(_swayX, _swayY, 0);
            _targetRotation = _initialRotation *
                              Quaternion.Euler(_swayX * _xSwayMultiplier, 0, _swayY * _zSwayMultiplier);
            transform.localPosition =
                Vector3.Lerp(transform.localPosition, _targetPosition, Time.deltaTime * _smoothAmount);
            transform.localRotation =
                Quaternion.Slerp(transform.localRotation, _targetRotation, Time.deltaTime * _smoothAmount);
        }
    }
}