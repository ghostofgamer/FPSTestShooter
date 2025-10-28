using System.Collections;
using PlayerContent;
using UI.Screens;
using UnityEngine;

namespace Initialization
{
    public class BootStrap : MonoBehaviour
    {
        [SerializeField] private GameSession _gameSession;
        [SerializeField] private PlayerLifeSystem _playerLifeSystem;
        [SerializeField] private GameOverScreen _gameOverScreen;
        [SerializeField] private Player _player;
        [SerializeField]private PlayerLook _playerLook;

        [SerializeField] private LoadingScreen _loadingScreen;
        
        private void Awake()
        {
            Initialization();
        }

        private void Initialization()
        {
            StartCoroutine(InitGame());
        }

        private IEnumerator InitGame()
        {
            _loadingScreen.Show();
             
            _loadingScreen.SetProgress(0.1f);
            
            yield return new WaitForSeconds(0.1f);
            
            _loadingScreen.SetProgress(0.4f);
            
            yield return null;
            _player.gameObject.SetActive(true);
            _gameSession.Init(_gameOverScreen, _playerLifeSystem);
            
            _loadingScreen.SetProgress(0.6f);
            
            yield return new WaitForSeconds(0.3f);
            
            _loadingScreen.SetProgress(1f);
            
            yield return _loadingScreen.FadeOut();
            _playerLook.enabled = true;
            
        }
    }
}