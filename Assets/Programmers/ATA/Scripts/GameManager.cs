
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

        private GameState _gameState;
        
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
            if (_gameState == state) return;
            
            _gameState = state;

            OnGameStateChanged?.Invoke(_gameState);
        }
        
        public void TogglePause()
        {
            if(_gameState == GameState.Playing)
                ChangeState(GameState.Paused);
            else if (_gameState == GameState.Paused)
                ChangeState(GameState.Playing);
        }
        
        
    }
