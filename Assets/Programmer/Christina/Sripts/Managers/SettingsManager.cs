using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        if(PlayerPrefs.HasKey("MusicVolume"))
        {
            LoadVolumes();
        }
        else
        {
            SoundManager.Instance.SetMusicVolume(musicSlider.value);
            SoundManager.Instance.SetSFXVolume(sfxSlider.value);
        } 
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

    void LoadVolumes()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");

        SoundManager.Instance.SetMusicVolume(musicSlider.value);
        SoundManager.Instance.SetSFXVolume(sfxSlider.value);
    }
}
