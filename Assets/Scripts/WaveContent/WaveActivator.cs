using UnityEngine;

namespace WaveContent
{
    public class WaveActivator : MonoBehaviour
    {
        [SerializeField] private WaveSequence _waveSequence;
        [SerializeField] private WaveMonitor waveMonitor;

        private void OnEnable()
        {
            waveMonitor.WaveCleared += ActivateWave;
        }

        private void OnDisable()
        {
            waveMonitor.WaveCleared -= ActivateWave;
        }

        public void ActivateWave()
        {
            _waveSequence.StartNextWave();
        }
    }
}