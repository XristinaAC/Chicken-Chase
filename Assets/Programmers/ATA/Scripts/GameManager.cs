
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

        public void ChangeState(GameState state)
        {
            if (CurrentState == state) return;
            CurrentState = state;

            OnGameStateChanged?.Invoke(CurrentState);
        }
    
        public void TogglePause()
        {
            if(CurrentState == GameState.Playing)
                ChangeState(GameState.Paused);
            else if (CurrentState == GameState.Paused)
                ChangeState(GameState.Playing);
        }
        
        
    }
