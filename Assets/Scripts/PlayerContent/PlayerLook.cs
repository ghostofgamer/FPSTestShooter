using UnityEngine;

namespace PlayerContent
{
    public class PlayerLook : MonoBehaviour
    {
        [SerializeField] private float _mouseSensitivity = 100f;
        [SerializeField] private Transform _playerBody;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private float  _minXRotate;
        [SerializeField] private float  _maxXRotate;
        
        private float xRotation = 0f;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            Rotate(_playerInput.MouseXValue, _playerInput.MouseYValue);
        }

        private void Rotate(float mouseXValue, float mouseYValue)
        {
            float mouseX = mouseXValue * _mouseSensitivity * Time.deltaTime;
            float mouseY = mouseYValue * _mouseSensitivity * Time.deltaTime;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, _minXRotate, _maxXRotate);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            _playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}