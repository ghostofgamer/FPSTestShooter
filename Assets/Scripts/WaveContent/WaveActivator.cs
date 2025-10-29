using UnityEngine;

namespace WaveContent
{
    public class WaveActivator : MonoBehaviour
    {
        [SerializeField] private WaveSequence _waveSequence;

        public void ActivateWave()
        {
            _waveSequence.StartNextWave();
        }
    }
}