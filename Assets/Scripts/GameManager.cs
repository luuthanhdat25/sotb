using System;
using System.Collections;
using Damage.RhythmScripts;
using Objects.UI.HUD;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class GameManager : RepeatMonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        [SerializeField] private int score = 0;
        [SerializeField] private int deathCount = 0;
        [SerializeField] private AudioSourcesManager audioSourcesManager;
        [SerializeField] private RepeatSceneManager sceneManager;

        [SerializeField] private bool isLoss = false;
        public bool IsLoss => isLoss;
        
        public UnityEvent onScoreChanged;
        public UnityEvent onDeath;
        public enum GameState
        {
            Started,
            Pause,
            WinGame,
            PlayerDie
        }
        private GameState gameState = GameState.Started;
        
        
        protected override void Awake()
        {
            if(Instance != null) Debug.LogError("There is more than one PlayerCtrl instance");
            Instance = this;
        }
        
        public void GameOver()
        {
            gameState = GameState.PlayerDie;
            audioSourcesManager.MusicFadeOut();
            StartCoroutine(DelayGameOver());
        }

        IEnumerator DelayGameOver()
        {
            yield return new WaitForSeconds(2f);
            Debug.Log(score);
            HUDManager.Instance.TotalScore(score);
            sceneManager.LossGame();
        }
        
        public void WinGame()
        {
            gameState = GameState.WinGame;
            audioSourcesManager.MusicFadeOut();
            StartCoroutine(DelayWinGame());
        }
        
        private IEnumerator DelayWinGame()
        {
            yield return new WaitForSeconds(4);
            HUDManager.Instance.TotalScore(score);
            sceneManager.WinGame();
        }
        
        public void IncreaseScore(int amount)
        {
            score += amount;
            onScoreChanged?.Invoke();
        }
        
        public void IncreaseDeathCount()
        {
            deathCount++;
            onDeath?.Invoke();
        }

        public int GetScore() => this.score;
    }
}