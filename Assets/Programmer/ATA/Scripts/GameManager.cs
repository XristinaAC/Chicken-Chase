
    using System;
    using UnityEngine;

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public enum GameState
        {
            MainMenu,
            Playing,
            Paused,
            GameOver,
        }

        public GameState CurrentState { get; private set; }

        public event Action<GameState> OnGameStateChanged;

        private void Awake()
        {
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
                    TimerManager.Instance.StartTimer();
                    break;

                case GameState.Paused:
                case GameState.GameOver:
                    TimerManager.Instance.StopTimer();
                    break;
            }
        }
        
        
    }
