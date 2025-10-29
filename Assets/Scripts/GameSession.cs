using System.Collections;
using PlayerContent;
using UI.Screens;
using UnityEngine;
using WaveContent;

public class GameSession : MonoBehaviour
{
    private GameOverScreen _gameOverScreen;
    private WinnerScreen _winnerScreen;
    private PlayerLifeSystem _playerLifeSystem;
    private WaveSequence _waveSequence;

    private void OnDisable()
    {
        _playerLifeSystem.PlayerDied -= OnPlayerDead;
        _waveSequence.AllWavesCompleted -= OnWinGame;
    }

    public void Init(GameOverScreen gameOverScreen, PlayerLifeSystem playerLifeSystem, WaveSequence waveSequence,
        WinnerScreen winnerScreen)
    {
        _playerLifeSystem = playerLifeSystem;
        _gameOverScreen = gameOverScreen;
        _winnerScreen = winnerScreen;
        _waveSequence = waveSequence;
        _playerLifeSystem.PlayerDied += OnPlayerDead;
        _waveSequence.AllWavesCompleted += OnWinGame;
    }

    private void OnPlayerDead()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        _gameOverScreen.Open();
    }

    private void OnWinGame()
    {
        if (_playerLifeSystem.Health.IsDead)
        {
            OnPlayerDead();
            return;
        }

        StartCoroutine(DelayedWinScreen());
    }

    private IEnumerator DelayedWinScreen()
    {
        yield return new WaitForSeconds(0.3f);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        _winnerScreen.Open();
    }
}