using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance = null;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider masterSlider;

    [SerializeField] GameObject settingsMenu;

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
        }
    }

    void LoadVolumes()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");

        SoundManager.Instance.SetMusicVolume(musicSlider.value);
        SoundManager.Instance.SetSFXVolume(sfxSlider.value);
        SoundManager.Instance.SetMasterVolume(masterSlider.value);
    }
}
