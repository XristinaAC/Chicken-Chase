using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        SoundManager.Instance.PlayMusic(SoundManager.backgroundAudio.backgroundMusic);
    }

    public void PlayGame()
    {
        SoundManager.Instance.PlaySFX(SoundManager.effectsAudio.buttonAudioEffect);
        if (GameManager.Instance != null)
            GameManager.Instance.ChangeState(GameManager.GameState.Playing);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        SoundManager.Instance.PlaySFX(SoundManager.effectsAudio.buttonAudioEffect);
        Application.Quit();
    }
}
