using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance = null;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider backgroundSFXSlider;

    Slider[] sliders;

    Slider master;
    Slider sfx;
    Slider music;

    private void Awake()
    {
        if (Instance == null)
        {
            //Instance = this;
        }
        else if (Instance != this)
        {
            //Destroy(this.gameObject);
        }
        //DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    { 
        SetVolumes();
        if(SceneManager.GetActiveScene().name == "Boss")
        {
            SoundManager.Instance.PlayMusic(SoundManager.backgroundAudio.bossRoomMusic);
        }
        else if(SceneManager.GetActiveScene().name == "AtaMainMenu")
        {
            SoundManager.Instance.PlayMusic(SoundManager.backgroundAudio.backgroundMusic);
        }
        else
        {
            SoundManager.Instance.PlayMusic(SoundManager.backgroundAudio.levelMusic);
            SoundManager.Instance.PlayBackgroundSFX(SoundManager.effectsAudio.waterEffect);
            SoundManager.Instance.PlayBackgroundSFX(SoundManager.effectsAudio.sizzlingPan);
        }
    }

    public void MasterSlider()
    {
        SoundManager.Instance.SetMasterVolume(masterSlider.value);
        PlayerPrefs.SetFloat("MasterVolume", masterSlider.value);
    }

    public void MusicSlider()
    {
        SoundManager.Instance.SetMusicVolume(musicSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    public void SFXSlider()
    {
        SoundManager.Instance.SetSFXVolume(sfxSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
    }

    public void BackgroundSFXSlider()
    {
        SoundManager.Instance.SetBackgroundSFXVolume(backgroundSFXSlider.value);
        PlayerPrefs.SetFloat("BackgroundSFX", backgroundSFXSlider.value);
    }

    public void SetVolumes()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            LoadVolumes();
        }
        else
        {
            SoundManager.Instance.SetMusicVolume(musicSlider.value);
            SoundManager.Instance.SetSFXVolume(sfxSlider.value);
            SoundManager.Instance.SetMasterVolume(masterSlider.value);
            SoundManager.Instance.SetBackgroundSFXVolume(backgroundSFXSlider.value);
        }
    }

    void LoadVolumes()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        backgroundSFXSlider.value = PlayerPrefs.GetFloat("BackgroundSFX");

        SoundManager.Instance.SetMusicVolume(musicSlider.value);
        SoundManager.Instance.SetSFXVolume(sfxSlider.value);
        SoundManager.Instance.SetMasterVolume(masterSlider.value);
        SoundManager.Instance.SetBackgroundSFXVolume(backgroundSFXSlider.value);
    }
}
