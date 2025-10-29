using System.Collections;
using ObjectPoolContent;
using PlayerContent;
using UI.Screens;
using UnityEngine;
using WaveContent;

namespace Initialization
{
    public class BootStrap : MonoBehaviour
    {
        [SerializeField] private GameSession _gameSession;
        [SerializeField] private PlayerLifeSystem _playerLifeSystem;
        [SerializeField] private GameOverScreen _gameOverScreen;
        [SerializeField] private Player _player;
        [SerializeField] private PlayerLook _playerLook;
        [SerializeField] private EnemyPools _enemyPools;
        [SerializeField] private LoadingScreen _loadingScreen;
        [SerializeField] private WinnerScreen _winnerScreen;
        [SerializeField] private WaveSequence _waveSequence;
        [SerializeField] private WaveActivator _waveActivator;

        private void Awake()
        {
            Initialization();
        }

        private void Initialization()
        {
            Time.timeScale = 1;
            StartCoroutine(InitGame());
        }

        private IEnumerator InitGame()
        {
            _loadingScreen.Show();
            _enemyPools.Init();
            _loadingScreen.SetProgress(0.1f);

            yield return new WaitForSeconds(0.1f);

            _loadingScreen.SetProgress(0.4f);

            yield return null;
            _player.gameObject.SetActive(true);
            _gameSession.Init(_gameOverScreen, _playerLifeSystem, _waveSequence, _winnerScreen);

            _loadingScreen.SetProgress(0.6f);

            yield return new WaitForSeconds(0.3f);

            _loadingScreen.SetProgress(1f);

            yield return _loadingScreen.FadeOut();
            _playerLook.enabled = true;


            _waveActivator.ActivateWave();
        }
    }
}