using System;
using System.Collections;
using Damage.RhythmScripts;
using Objects.UI.HUD;
using Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class GameManager : RepeatMonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        [SerializeField] private int score = 0;
        [SerializeField] private RepeatSceneManager sceneManager;
 
        private enum GameState
        {
            Started, Pause,
            PlayerDead,
            WinGame, LossGame
        }
        
        private GameState gameState;
        
        public class ScoreEventArgs : EventArgs
        {
            public int Score { get; set; }
        }
        
        public event EventHandler<ScoreEventArgs> ScoreChangedEvent;
        public event EventHandler<ScoreEventArgs> ScoreResultsEvent;
        
        //public event EventHandler OnPauseGame;
        //public event EventHandler OnContinueGame;
        //public event EventHandler PlayerDead;
        public event EventHandler WinGameEvent;
        public event EventHandler LossGameEvent;
        
        
        protected override void Awake()
        {
            if(Instance != null) Debug.LogError("There is more than one PlayerCtrl instance");
            Instance = this;

            gameState = GameState.Started;
        }
        
        public void WinGame()
        {
            gameState = GameState.WinGame;
            WinGameEvent?.Invoke(this, EventArgs.Empty);
            StartCoroutine(DelayWinGame());
        }
        
        private IEnumerator DelayWinGame()
        {
            yield return new WaitForSeconds(4);
            ScoreResultsEvent?.Invoke(this, new ScoreEventArgs(){Score = this.score});
            ////
            if(score > 0) AudioSpawner.Instance.ScoreRaiseSound(true);
            sceneManager.WinGame();
        }
        
        public void GameOver()
        {
            gameState = GameState.LossGame;
            LossGameEvent?.Invoke(this, EventArgs.Empty);
            StartCoroutine(DelayGameOver());
        }

        IEnumerator DelayGameOver()
        {
            yield return new WaitForSeconds(2f);
            ScoreResultsEvent?.Invoke(this, new ScoreEventArgs(){Score = this.score});
            if(score > 0) AudioSpawner.Instance.ScoreRaiseSound(true);
            AudioSpawner.Instance.UIEffect();
            sceneManager.LossGame();
        }
        
        
        
        public void IncreaseScore(int amount)
        {
            score += amount;
            ScoreChangedEvent?.Invoke(this, new ScoreEventArgs(){Score = this.score});
        }
        
        public int GetScore() => this.score;
        
        public bool IsFinishGame() => gameState != GameState.Started;
    }
}
