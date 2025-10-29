using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WaveContent
{
    public class WaveViewer : MonoBehaviour
    {
        [SerializeField] private WaveSequence _waveSequence;
        [SerializeField] private WaveMonitor _waveMonitor;
        [SerializeField] private Image _progressBar;
        [SerializeField] private TMP_Text _countdownText;
        [SerializeField] private TMP_Text _enemyAmountText;
        [SerializeField] private TMP_Text _waveCountText;

        private void OnEnable()
        {
            _waveSequence.OnCountdownTick += HandleCountdown;
            _waveSequence.OnWaveStarted += HandleWaveStarted;
            _waveSequence.ProgressChanged += HandleWaveProgressChanged;
            _waveMonitor.EnemyAmountChanged += HandlerEnemyWaveAmount;
        }

        private void OnDisable()
        {
            _waveSequence.OnCountdownTick -= HandleCountdown;
            _waveSequence.OnWaveStarted -= HandleWaveStarted;
            _waveSequence.ProgressChanged -= HandleWaveProgressChanged;
            _waveMonitor.EnemyAmountChanged += HandlerEnemyWaveAmount;
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
            _enemyAmountText.gameObject.SetActive(false);
            _countdownText.text = $"Следующая волна через: {Mathf.CeilToInt(timeLeft)}";
        }

        private void HandleWaveStarted(int amount)
        {
            _waveCountText.text = $"Волна: {(_waveSequence.CurrentWaveIndex + 1).ToString()}";
            _countdownText.gameObject.SetActive(false);
            _enemyAmountText.gameObject.SetActive(true);
            _progressBar.fillAmount = 0;
            HandlerEnemyWaveAmount(amount);
        }

        private void HandlerEnemyWaveAmount(int amount)
        {
            _enemyAmountText.text = $"Врагов осталось : {amount.ToString()}";
        }
    }
}