using UnityEngine;

namespace PlayerContent
{
    public class PlayerLook : MonoBehaviour
    {
// @formatter:off
        [Header("References")]
        [SerializeField] private Transform _playerBody;
        [SerializeField] private PlayerInput _playerInput;
        [Header("Settings")]
        [SerializeField] private float _mouseSensitivity = 100f;
        [SerializeField] private float _minXRotate;
        [SerializeField] private float _maxXRotate;
// @formatter:on

        private float xRotation = 0f;
        private float _mouseX;
        private float _mouseY;
        private float _zero = 0f;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            xRotation = _zero;
            transform.localRotation = Quaternion.Euler(xRotation, _zero, _zero);
        }

        private void Update()
        {
            Rotate(_playerInput.MouseXValue, _playerInput.MouseYValue);
        }

        private void Rotate(float mouseXValue, float mouseYValue)
        {
            _mouseX = mouseXValue * _mouseSensitivity * Time.deltaTime;
            _mouseY = mouseYValue * _mouseSensitivity * Time.deltaTime;
            xRotation -= _mouseY;
            xRotation = Mathf.Clamp(xRotation, _minXRotate, _maxXRotate);
            transform.localRotation = Quaternion.Euler(xRotation, _zero, _zero);
            _playerBody.Rotate(Vector3.up * _mouseX);
        }
    }
}