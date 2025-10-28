using PlayerContent;
using UI.Screens;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    private GameOverScreen _gameOverScreen;
    private PlayerLifeSystem _playerLifeSystem;

    private void OnDisable()
    {
        _playerLifeSystem.PlayerDied -= OnPlayerDead;
    }

    public void Init(GameOverScreen gameOverScreen, PlayerLifeSystem playerLifeSystem)
    {
        Time.timeScale = 1;
        _playerLifeSystem = playerLifeSystem;
        _gameOverScreen = gameOverScreen;
        _playerLifeSystem.PlayerDied += OnPlayerDead;
    }

    private void OnPlayerDead()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        _gameOverScreen.Open();
    }
}