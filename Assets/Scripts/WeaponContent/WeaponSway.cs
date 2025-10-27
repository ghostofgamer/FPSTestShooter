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
        [SerializeField] private float _swayAmount = 0.05f;       // Насколько оружие наклоняется
        [SerializeField] private float _maxSwayAmount = 0.1f;     // Ограничение наклона
        [SerializeField] private float _smoothAmount = 8f;
        [SerializeField] private float _xSwayMultiplier = 20f; // наклон вперед/назад
        [SerializeField] private float _ySwayMultiplier = 10f; // поворот влево/вправо
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
            
            /*// Берём движение мыши
            float mouseX = _playerInput.MouseXValue;
            float mouseY = _playerInput.MouseYValue;

            // Вычисляем смещение (чем больше движение, тем сильнее наклон)
            float swayX = Mathf.Clamp(-mouseX * _swayAmount, -_maxSwayAmount, _maxSwayAmount);
            float swayY = Mathf.Clamp(-mouseY * _swayAmount, -_maxSwayAmount, _maxSwayAmount);

            // Целевая позиция и вращение
            Vector3 targetPosition = _initialPosition + new Vector3(swayX, swayY, 0);
            Quaternion targetRotation = _initialRotation *
                                        Quaternion.Euler(swayY * _xSwayMultiplier, swayX * _ySwayMultiplier,
                                            swayX * _zSwayMultiplier);


            // Плавно двигаем оружие к цели
            transform.localPosition =
                Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * _smoothAmount);
            transform.localRotation =
                Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * _smoothAmount);*/

            float mouseX = _playerInput.MouseXValue;
            float mouseY = _playerInput.MouseYValue;

            // Считаем смещения
            float swayX = Mathf.Clamp(-mouseX * _swayAmount, -_maxSwayAmount, _maxSwayAmount);
            float swayY = Mathf.Clamp(-mouseY * _swayAmount, -_maxSwayAmount, _maxSwayAmount);

            // Позиция: можно слегка сдвигать по X/Y, или оставить только X для простого sway
            Vector3 targetPosition = _initialPosition + new Vector3(swayX, swayY, 0);

            // Ротация оружия:
            // - X = наклон при боковых движениях (mouseX)
            // - Z = крен при вертикальных движениях (mouseY)
            // - Y оставляем 0 или можно добавить небольшую реакцию, если нужно
            Quaternion targetRotation = _initialRotation *
                                        Quaternion.Euler(swayX * _xSwayMultiplier, 0, swayY * _zSwayMultiplier);

            // Плавное движение
            transform.localPosition =
                Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * _smoothAmount);
            transform.localRotation =
                Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * _smoothAmount);
        }
    }
}