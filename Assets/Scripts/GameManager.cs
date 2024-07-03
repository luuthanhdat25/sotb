using System;
using System.Collections;
using Damage.RhythmScripts;
using Objects.UI.HUD;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameManager : RepeatMonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private bool isLevelOne = false;
        [SerializeField] private int score = 0;
        [SerializeField] private float timeFinished = 0;
        [SerializeField] private RepeatSceneManager sceneManager;

        private bool isStopTimer = true;
        
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
        
        public class TimeFinishEventArgs : EventArgs
        {
            public float Time { get; set; }
        }
        
        public event EventHandler<ScoreEventArgs> ScoreChangedEvent;
        public event EventHandler<ScoreEventArgs> ScoreResultsEvent;
        public event EventHandler<TimeFinishEventArgs> TimeResultsEvent;
        
        public event EventHandler WinGameEvent;
        public event EventHandler LossGameEvent;
        
        
        protected override void Awake()
        {
            if(Instance != null) Debug.LogError("There is more than one PlayerCtrl instance");
            Instance = this;
            
            gameState = GameState.Started;
        }

        private void Start()
        {
            if (DataManager.Instance == null) return;
            if (isLevelOne) DataManager.Instance.SaveData(0, 0);
            LoadData();
            
            //Update UI
            IncreaseScore(0);
            UIManager.Instance.UpdateTimerUI(timeFinished);
        }
        
        private void LoadData()
        {
            this.score = DataManager.Instance.GetScore();
            this.timeFinished = DataManager.Instance.GetTimeFinished();
        }
        
        private void FixedUpdate()
        {
            if(!isStopTimer) 
                UpdateTimeCounter();
        }

        private void UpdateTimeCounter()
        {
            timeFinished += Time.fixedDeltaTime;
            UIManager.Instance.UpdateTimerUI(timeFinished);
        }
        
        public void WinGame()
        {
            gameState = GameState.WinGame;
            WinGameEvent?.Invoke(this, EventArgs.Empty);
            StartCoroutine(DelayWinGame());
        }
        
        private IEnumerator DelayWinGame()
        {
            yield return new WaitForSeconds(5f);
            ScoreResultsEvent?.Invoke(this, new ScoreEventArgs(){Score = this.score});
            TimeResultsEvent?.Invoke(this, new TimeFinishEventArgs(){Time = this.timeFinished});
            
            if(score > 0) AudioSpawner.Instance.ScoreRaiseSound(true);
            sceneManager.WinGame();
            
            //SaveDataJson();
            DataManager.Instance?.SaveData(this.score, this.timeFinished);
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

        public void SetIsStopTimer(bool isOn) => this.isStopTimer = isOn;
        
        public int GetScore() => this.score;
        
        public bool IsFinishGame() => gameState == GameState.WinGame || gameState == GameState.LossGame;

        public void PauseGame() => this.gameState = GameState.Pause;

        public void ContinueGame() => this.gameState = GameState.Started;

        public bool IsPauseGame() => this.gameState == GameState.Pause;
    }
}
