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
        [SerializeField] private int deathCount = 0;
        [FormerlySerializedAs("audioManager")] [SerializeField] private AudioSpawner audioSpawner;
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
            AudioSpawner.Instance.FadeOutMusic(5);
            StartCoroutine(DelayGameOver());
        }

        IEnumerator DelayGameOver()
        {
            yield return new WaitForSeconds(2f);
            UsersInterfaceManager.Instance.TotalScore(score);
            AudioSpawner.Instance.UIEffect();
            if(score > 0) AudioSpawner.Instance.ScoreRaiseSound();
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
            UsersInterfaceManager.Instance.TotalScore(score);
            if(score > 0) AudioSpawner.Instance.ScoreRaiseSound();
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