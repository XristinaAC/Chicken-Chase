using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private string firstLevelName = "Level_1";

    private void OnEnable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameManager.GameState state)
    {
        gameOverPanel.SetActive(state == GameManager.GameState.GameOver);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
        GameManager.Instance.ChangeState(GameManager.GameState.MainMenu);
    }
    public async void RestartLevel()
    {
        if (LevelManager.Instance != null)
            await LevelManager.Instance.LoadLevelAsync(firstLevelName);
        else
            SceneManager.LoadScene(firstLevelName); // fallback

        TimerManager.Instance?.ResetTimer();
        GameManager.Instance?.ChangeState(GameManager.GameState.Playing);
    }
}