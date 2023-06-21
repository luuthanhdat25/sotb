using System.Collections;
using Damage.RhythmScripts;
using Objects.UI.HUD;
using Player;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class GameManager : RepeatMonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        [SerializeField] private int score = 0;
        [SerializeField] private int deathCount = 0;
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private RepeatSceneManager sceneManager;
        
        public UnityEvent onScoreChanged;
        public UnityEvent onDeath;
        
        
        private enum GameState
        {
            Started,
            Pause,
            WinGame,
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
            AudioManager.Instance.MusicFadeOut();
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
            AudioManager.Instance.MusicFadeOut();
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