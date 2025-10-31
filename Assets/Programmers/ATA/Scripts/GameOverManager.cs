using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;

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
        SceneManager.LoadScene("MainMenu");
        GameManager.Instance.ChangeState(GameManager.GameState.MainMenu);
    }
}