using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;

    [System.Serializable]
    public enum backgroundAudio
    {
        backgroundMusic,
        levelMusic,
        bossRoomMusic
    }

    [System.Serializable]
    public enum effectsAudio
    {
        deathAudioEffect,
        buttonAudioEffect,
        jumpingAudioEffect,
        glidingAudioEffect,
        landAudioEffect,
        uiHoverAudioEffect,
        runningAudioEffect,
        waterEffect
    }

    [System.Serializable]
    public struct BackgroundMusic
    {
        public backgroundAudio type;
        public AudioClip clip;
    }
    [System.Serializable]
    public struct SoundEffects
    {
        public effectsAudio type;
        public AudioClip clip;
    }

    [Header("_____________AudioSource_______________")]

    [SerializeField] private AudioSource MusicSource;
    [SerializeField] private AudioSource SFXSource;
    [SerializeField] private AudioMixer audioMixer;


    [Header("_____________AudioClips_______________")]

    [SerializeField] List<BackgroundMusic> music;
    [SerializeField] public List<SoundEffects> effects;
    [SerializeField] public AudioClip backgroundMusic;
    [SerializeField] public AudioClip buttonEffect;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            //Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void PlayMusic(AudioClip music)
    {
        MusicSource.clip = music;
        MusicSource.Play();
    }

    public void PlayMusic(backgroundAudio musicT)
    {
        for(int i=0; i< music.Count; i++)
        {
            if(musicT == music[i].type)
            {
                MusicSource.clip = music[i].clip;
                MusicSource.Play();
            }
        }
    }

    public void PlaySFX(AudioClip sfxEffect)
    {
        SFXSource.clip = sfxEffect;
        SFXSource.Play();
    }

    public void PlaySFX(effectsAudio effectT)
    {
        for (int i = 0; i < effects.Count; i++)
        {
            if (effectT == effects[i].type)
            {
                SFXSource.clip = effects[i].clip;
                SFXSource.Play();
            }
        }
    }

    public void StopPlayingMusic(backgroundAudio musicT)
    {
        for (int i = 0; i < music.Count; i++)
        {
            if (musicT == music[i].type)
            {
                MusicSource.clip = music[i].clip;
                MusicSource.Stop();
            }
        }
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume)*20);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }
}
