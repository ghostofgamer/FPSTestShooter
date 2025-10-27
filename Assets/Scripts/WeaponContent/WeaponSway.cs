using PlayerContent;
using UnityEngine;

namespace WeaponContent
{
    public class WeaponSway : MonoBehaviour
    {
// @formatter:off
        [Header("References")]
        [SerializeField] private PlayerInput _playerInput;   
        [Header("Sway Settings")]
        [SerializeField] private float _swayAmount = 0.05f;      
        [SerializeField] private float _maxSwayAmount = 0.1f;     
        [SerializeField] private float _smoothAmount = 8f;
        [SerializeField] private float _xSwayMultiplier = 20f; 
        [SerializeField] private float _ySwayMultiplier = 10f; 
        [SerializeField] private float _zSwayMultiplier = 15f;
        
        private Vector3 _initialPosition;
        private Quaternion _initialRotation;
// @formatter:on

        private void Start()
        {
            _initialPosition = transform.localPosition;
            _initialRotation = transform.localRotation;
        }

        private void Update()
        {
            if (_playerInput.IssAiming)
                return;

            float swayX = Mathf.Clamp(-_playerInput.MouseXValue * _swayAmount, -_maxSwayAmount, _maxSwayAmount);
            float swayY = Mathf.Clamp(-_playerInput.MouseYValue * _swayAmount, -_maxSwayAmount, _maxSwayAmount);
            Vector3 targetPosition = _initialPosition + new Vector3(swayX, swayY, 0);
            Quaternion targetRotation = _initialRotation *
                                        Quaternion.Euler(swayX * _xSwayMultiplier, 0, swayY * _zSwayMultiplier);
            transform.localPosition =
                Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * _smoothAmount);
            transform.localRotation =
                Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * _smoothAmount);
        }
    }
}