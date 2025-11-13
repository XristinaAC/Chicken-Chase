using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class PauseManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button settingsMenuButton;
    [SerializeField] private Button closeSettingsMenu;
    [SerializeField] private GameObject settingsMenu;

    private void Awake()
    {
        pauseMenuUI.SetActive(false);
    }

    private void OnEnable()
    {
        resumeButton?.onClick.AddListener(ResumeGame);
        mainMenuButton?.onClick.AddListener(ReturnToMainMenu);
        settingsMenuButton?.onClick.AddListener(OpenSettingsMenu);
        closeSettingsMenu?.onClick.AddListener(CloseSettingsMenu);
    }

    private void OnDisable()
    {
        resumeButton?.onClick.RemoveListener(ResumeGame);
        mainMenuButton?.onClick.RemoveListener(ReturnToMainMenu);
        settingsMenuButton?.onClick.RemoveListener(OpenSettingsMenu);
        closeSettingsMenu?.onClick.RemoveListener(CloseSettingsMenu);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    private void TogglePause()
    {
        bool isPaused = GameManager.Instance.CurrentState == GameManager.GameState.Paused;
        SetPausedState(!isPaused);
    }

    private void ResumeGame() => SetPausedState(false);

    private void SetPausedState(bool pause)
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.buttonEffect);
        if (pause)
        {
            GameManager.Instance.ChangeState(GameManager.GameState.Paused);
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            GameManager.Instance.ChangeState(GameManager.GameState.Playing);
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    private void ReturnToMainMenu()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.buttonEffect);
        GameManager.Instance.ChangeState(GameManager.GameState.MainMenu);
        LevelManager.Instance.ResetGameState();
        SceneManager.LoadScene(0);
    }

    public void OpenSettingsMenu()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.buttonEffect);
        settingsMenu.SetActive(true);
    }

    public void CloseSettingsMenu()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.buttonEffect);
        settingsMenu.SetActive(false);
    }
}