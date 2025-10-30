using UnityEngine;
using UnityEngine.UI;

namespace HealthContent
{
    public class HealthViewer : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private Image _healthBar;

        private float _fill;

        private void OnEnable()
        {
            _health.HealthChanged += OnHealthChanged;
        }

        private void OnDisable()
        {
            _health.HealthChanged += OnHealthChanged;
        }

        private void OnHealthChanged(int current, int max)
        {
            if (_healthBar == null) return;

            _fill = (float)current / max;
            _healthBar.fillAmount = Mathf.Clamp01(_fill);
        }
    }
}