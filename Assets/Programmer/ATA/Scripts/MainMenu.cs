using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        SoundManager.Instance.PlayMusic(SoundManager.Instance.backgroundMusic);
    }

    public void PlayGame()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.buttonEffect);
        if (GameManager.Instance != null)
            GameManager.Instance.ChangeState(GameManager.GameState.Playing);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.buttonEffect);
        Application.Quit();
    }
}
