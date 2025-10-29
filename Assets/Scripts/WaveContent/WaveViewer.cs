using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WaveContent
{
    public class WaveViewer : MonoBehaviour
    {
        [SerializeField] private WaveSequence _waveSequence;
        [SerializeField] private Image _progressBar;
        [SerializeField] private TMP_Text _countdownText;
        
        private void OnEnable()
        {
            _waveSequence.OnCountdownTick += HandleCountdown;
            _waveSequence.OnWaveStarted += HandleWaveStarted;
            _waveSequence.ProgressChanged += HandleWaveProgressChanged;
            // _waveSequence.OnAllWavesCompleted += HandleAllWavesCompleted;
        }

        private void OnDisable()
        {
            _waveSequence.OnCountdownTick -= HandleCountdown;
            _waveSequence.OnWaveStarted -= HandleWaveStarted;
            _waveSequence.ProgressChanged -= HandleWaveProgressChanged;
            // _waveSequence.OnAllWavesCompleted -= HandleAllWavesCompleted;
        }

        private void HandleWaveProgressChanged(int current, int total)
        {
            if (total <= 0)
                return;
            
            _progressBar.fillAmount = (float)current / total;
        }
        
        private void HandleCountdown(float timeLeft)
        {
            _countdownText.gameObject.SetActive(true);
            _countdownText.text = $"Следующая волна через: {Mathf.CeilToInt(timeLeft)}";
        }
        
        private void HandleWaveStarted()
        {
            _countdownText.gameObject.SetActive(false);
            _progressBar.fillAmount = 0;
        }
    }
}