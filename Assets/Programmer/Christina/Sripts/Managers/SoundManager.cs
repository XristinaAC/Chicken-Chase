using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;

    public enum AudioTypes
    {
        backgroundMusic,
        death,
        buttonAudioEffects,
        jumpingAudioEffects,
        glidingAudioEffects,
    }

    [Header("_____________AudioSource_______________")]

    [SerializeField] private AudioSource MusicSource;
    [SerializeField] private AudioSource SFXSource;


    [Header("_____________AudioClips_______________")]

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
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    
    public void PlayMusic(AudioClip music)
    {
        MusicSource.clip = music;
        MusicSource.Play();
    }

    public void PlaySFX(AudioClip sfxEffect)
    {
        SFXSource.clip = sfxEffect;
        SFXSource.Play();
    }
}
