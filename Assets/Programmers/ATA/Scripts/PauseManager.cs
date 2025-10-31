using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    
    private float targetTimeScale = 1f;
    private void Awake()
    {
        pauseMenuUI.SetActive(false);
    }

    private void OnEnable()
    {
        resumeButton?.onClick.AddListener(ResumeGame);
        mainMenuButton?.onClick.AddListener(ReturnToMainMenu);
    }

    private void OnDisable()
    {
        resumeButton?.onClick.RemoveListener(ResumeGame);
        mainMenuButton?.onClick.RemoveListener(ReturnToMainMenu);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
           
        }
    }

    private void TogglePause()
    {
        bool isPaused = GameManager.Instance.CurrentState == GameManager.GameState.Paused;

        if (isPaused)
        {
            GameManager.Instance.ChangeState(GameManager.GameState.Playing);
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            GameManager.Instance.ChangeState(GameManager.GameState.Paused);
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void ResumeGame()
    {
        GameManager.Instance.ChangeState(GameManager.GameState.Playing);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    private void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        GameManager.Instance.ChangeState(GameManager.GameState.MainMenu);
        SceneManager.LoadScene(0);
    }
}