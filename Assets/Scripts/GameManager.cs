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
        public class ScoreEventArgs : EventArgs
        {
            public int Score { get; set; }
        }
        
        public static GameManager Instance { get; private set; }
        
        [SerializeField] private int score = 0;
        [SerializeField] private RepeatSceneManager sceneManager;
        
        
        public event EventHandler<ScoreEventArgs> OnScoreChanged;
        public event EventHandler<ScoreEventArgs> OnScoreResults;
        
        private enum GameState
        {
            Started, Pause, WinGame,
            GameOver,
        }
        private GameState gameState = GameState.Started;

        public bool IsFinishGame() => gameState != GameState.Started;
        
        protected override void Awake()
        {
            if(Instance != null) Debug.LogError("There is more than one PlayerCtrl instance");
            Instance = this;
        }
        
        public void GameOver()
        {
            gameState = GameState.GameOver;
            PlayerCtrl.Instance.PlayerMovement.SetCanMoveNormal(false);
            AudioSpawner.Instance.FadeOutMusic(5);
            StartCoroutine(DelayGameOver());
        }

        IEnumerator DelayGameOver()
        {
            yield return new WaitForSeconds(2f);
            OnScoreResults?.Invoke(this, new ScoreEventArgs(){Score = this.score});
            AudioSpawner.Instance.UIEffect();
            if(score > 0) AudioSpawner.Instance.ScoreRaiseSound(true);
            sceneManager.LossGame();
        }
        
        public void WinGame()
        {
            gameState = GameState.WinGame;
            PlayerCtrl.Instance.ItemMagnet.SetRadiusItemMagnet(10f);
            AudioSpawner.Instance.FadeOutMusic(5);
            StartCoroutine(DelayWinGame());
        }
        
        private IEnumerator DelayWinGame()
        {
            yield return new WaitForSeconds(4);
            OnScoreResults?.Invoke(this, new ScoreEventArgs(){Score = this.score});
            if(score > 0) AudioSpawner.Instance.ScoreRaiseSound(true);
            sceneManager.WinGame();
        }
        
        public void IncreaseScore(int amount)
        {
            score += amount;
            OnScoreChanged?.Invoke(this, new ScoreEventArgs(){Score = this.score});
        }
        
        public int GetScore() => this.score;
    }
}