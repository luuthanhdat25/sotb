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
        
        public UnityEvent onScoreChanged;
        public UnityEvent onDeath;
        public enum GameState
        {
            Started,
            Pause,
            WinGame,
            MainMenu,
            PlayerDie
        }
        private GameState gameState = GameState.MainMenu;
        
        protected override void Awake()
        {
            if(Instance != null) Debug.LogError("There is more than one PlayerCtrl instance");
            Instance = this;
        }
        
        public void Start()
        {
            if (gameState == GameState.MainMenu)
            {
                gameState = GameState.Started;
            }
        }
        
        public void GameOver()
        {
            gameState = GameState.PlayerDie;
            HUDManager.Instance.SetActiveEnergiesBar(false);
            HUDManager.Instance.SetActiveBoostceilBar(false);
            audioSourcesManager.MusicFadeOut();
            StartCoroutine(DelayGameOver());
        }

        IEnumerator DelayGameOver()
        {
            yield return new WaitForSeconds(2.5f);
            HUDManager.Instance.UpdateScoreLossUI(score);
            HUDManager.Instance.SetActiveLossUI(true);
            Time.timeScale = 0;
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
            HUDManager.Instance.UpdateScoreWinUI(score);
            HUDManager.Instance.SetActiveWinUI(true);
        }
        
        public void PlayAgain()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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