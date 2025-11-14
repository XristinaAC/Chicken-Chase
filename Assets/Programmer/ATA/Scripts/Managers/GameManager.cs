
    using System;
    using UnityEngine;
using UnityEngine.SceneManagement;

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public enum GameState
        {
            MainMenu,
            Playing,
            Paused,
            GameOver,
            MiniGame
        }

        public GameState CurrentState { get; private set; }

        public event Action<GameState> OnGameStateChanged;

        private void Awake()
        {
           
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 120;
            
            
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                ChangeState(GameState.MainMenu);
            }
            else 
                Destroy(gameObject);

        }

        private void Update()
        {
            Debug.Log("GameManager is " + CurrentState);
            Debug.Log("Timescale = " + Time.timeScale);
        }

        public void ChangeState(GameState state)
        {
            if (CurrentState == state) return;
            CurrentState = state;

            OnGameStateChanged?.Invoke(CurrentState);
            
            switch (state)
            {
                case GameState.MainMenu:
                    TimerManager.Instance.ResetTimer();
                    break;

                case GameState.Playing:
                case GameState.MiniGame:
                    TimerManager.Instance.StartTimer();
                    break;

                case GameState.Paused:
                case GameState.GameOver:
                    SoundManager.Instance.PlaySFX(SoundManager.effectsAudio.deathAudioEffect);
                    TimerManager.Instance.StopTimer();
                    break;
            }
        }

    private void CheckLevel()
    {
        if (SceneManager.GetActiveScene().name == "Boss")
        {
            SoundManager.Instance.StopPlayingMusic(SoundManager.backgroundAudio.backgroundMusic);
            SoundManager.Instance.PlayMusic(SoundManager.backgroundAudio.bossRoomMusic);
        }
    }
}
