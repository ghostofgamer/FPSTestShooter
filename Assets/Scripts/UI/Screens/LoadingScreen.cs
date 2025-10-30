using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class LoadingScreen : MonoBehaviour
    {
// @formatter:off
        [Header("UI Elements")] 
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image fillImage;
        [Header("Settings")]
        [SerializeField] private float fadeDuration = 0.5f;
        [SerializeField] private float fillSmoothSpeed = 3f;
// @formatter:on

        private float _targetFill = 0f;
        private bool _isVisible;
        private float _elapsedTime;
        private float _fullApha = 1f;
        private float _zeroAlpha = 0f;

        private void Awake()
        {
            canvasGroup.alpha = _fullApha;
            canvasGroup.blocksRaycasts = true;
            fillImage.fillAmount = 0f;
            _isVisible = true;
        }

        private void Update()
        {
            fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, _targetFill, fillSmoothSpeed * Time.deltaTime);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            canvasGroup.alpha = _fullApha;
            canvasGroup.blocksRaycasts = true;
            fillImage.fillAmount = _zeroAlpha;
            _isVisible = true;
        }

        public void SetProgress(float value)
        {
            _targetFill = Mathf.Clamp01(value);
        }

        public IEnumerator FadeOut()
        {
            _elapsedTime = 0f;

            while (_elapsedTime < fadeDuration)
            {
                _elapsedTime += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(_fullApha, _zeroAlpha, _elapsedTime / fadeDuration);
                yield return null;
            }

            canvasGroup.alpha = _zeroAlpha;
            canvasGroup.blocksRaycasts = false;
            gameObject.SetActive(false);
            _isVisible = false;
        }
    }
}