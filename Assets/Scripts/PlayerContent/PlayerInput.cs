using System;
using UnityEngine;

namespace PlayerContent
{
    public class PlayerInput : MonoBehaviour
    {
        private const string MouseX = "Mouse X";
        private const string MouseY = "Mouse Y";
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";
        private const string Jump = "Jump";
        
        private bool _isAiming;
        
        public float MouseXValue { get; private set; }
        public float MouseYValue { get; private set; }
        public float X { get; private set; }
        public float Z { get; private set; }
        public bool IsRunning { get; private set; }
        public bool IsCrouching { get; private set; }

        public event Action RKeyPressed;
        public event Action<bool> AimStateChanged;
        public event Action MouseLeftKeyPressed;
        public event Action MouseLeftKeyReleased;
        public event Action MouseZeroKeyHoldDown;
        public event Action PausePressed;
        public event Action JumpPressed;

        private void Update()
        {
            if (Time.timeScale > 0)
            {
                if (Input.GetKeyDown(KeyCode.R))
                    RKeyPressed?.Invoke();

                if (Input.GetMouseButtonDown(0))
                    MouseLeftKeyPressed?.Invoke();
                
                if (Input.GetMouseButtonUp(0))
                    MouseLeftKeyReleased?.Invoke();

                if (Input.GetMouseButtonDown(1))
                {
                    _isAiming = !_isAiming;
                    AimStateChanged?.Invoke(_isAiming);
                }

                if (Input.GetButtonDown(Jump))
                    JumpPressed?.Invoke();

                MouseXValue = Input.GetAxis(MouseX);
                MouseYValue = Input.GetAxis(MouseY);
                IsRunning = Input.GetKey(KeyCode.LeftShift);
                IsCrouching = Input.GetKey(KeyCode.LeftControl);
                X = Input.GetAxis(Horizontal);
                Z = Input.GetAxis(Vertical);
            }
        }
    }
}