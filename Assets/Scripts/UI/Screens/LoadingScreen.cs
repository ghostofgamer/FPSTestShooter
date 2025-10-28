using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class LoadingScreen : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image fillImage;
        [SerializeField] private float fadeDuration = 0.5f;
        [SerializeField] private float fillSmoothSpeed = 3f;

        private float _targetFill = 0f;
        private bool _isVisible;

        private void Awake()
        {
            canvasGroup.alpha = 1f;
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
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            fillImage.fillAmount = 0f;
            _isVisible = true;
        }

        public void SetProgress(float value)
        {
            _targetFill = Mathf.Clamp01(value);
        }

        public IEnumerator FadeOut()
        {
            float t = 0f;
            
            while (t < fadeDuration)
            {
                t += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
                yield return null;
            }

            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = false;
            gameObject.SetActive(false);
            _isVisible = false;
        }
    }
}