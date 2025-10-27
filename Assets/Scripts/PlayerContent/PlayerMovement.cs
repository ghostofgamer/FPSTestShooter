using UnityEngine;

namespace PlayerContent
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private float _speedWalk = 13;
        [SerializeField] private float _speedRun = 31;
        [SerializeField] private float _groundDistance = 0.4f;
        [SerializeField] private float _jumpHight;
        [SerializeField] private float _gravity = -9.81f;
        [SerializeField] private float _minHeight = 1.5f;
        [SerializeField] private float _maxHeight = 3f;
        [SerializeField] private float _factor = 2f;

        private float _x;
        private float _z;
        private Vector3 _move;
        private Vector3 _velocity;
        private bool _isGrounded;
        private float _speed;

        private void OnEnable()
        {
            _playerInput.JumpPressed += Jump;
        }

        private void OnDisable()
        {
            _playerInput.JumpPressed -= Jump;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            _speed = _playerInput.IsRunning && _isGrounded ? _speedRun : _speedWalk;
            _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);
            _characterController.height = _playerInput.IsCrouching ? _minHeight : _maxHeight;

            if (_isGrounded && _velocity.y < 0)
                _velocity.y = -_factor;

            _x = _playerInput.X;
            _z = _playerInput.Z;
            _move = transform.right * _x + transform.forward * _z;
            _characterController.Move(_move * _speed * Time.deltaTime);
            _velocity.y += _gravity * Time.deltaTime;
            _characterController.Move(_velocity * Time.deltaTime);
        }

        private void Jump()
        {
            if (_isGrounded)
                _velocity.y = Mathf.Sqrt(_jumpHight * -_factor * _gravity);
        }
    }
}