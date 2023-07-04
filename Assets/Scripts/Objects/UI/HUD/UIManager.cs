using System.Collections;
using Damage.RhythmScripts;
using DefaultNamespace;
using TMPro;
using UnityEngine;

namespace Objects.UI.HUD
{
    public class UIManager : RepeatMonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [SerializeField] private Transform energiesBar;
        [SerializeField] private Transform boostceilBar;
        
        [SerializeField] private TMP_Text totalScoreText;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private float scoreIncreaseDuration = 1.0f;
        private Animator animator;
        
        private int currentScore = 0;
        private const string IS_FADE_OUT = "isFadeOut";
        
        protected override void Awake()
        {
            if(Instance != null) Debug.LogError("There is more than one MiniBossKla_edCtrl instance");
            Instance = this;
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadAnimator();
        }
        
        private void LoadAnimator()
        {
            if(this.animator != null) return;
            this.animator = GetComponent<Animator>();
        }

        private void Start()
        {
            GameManager.Instance.OnScoreChanged += UpdatingScore;
            GameManager.Instance.OnScoreResults += TotalScoreResult;
        }
        
        public void SetActiveEnergiesBar(bool isTrue) => energiesBar.gameObject.SetActive(isTrue);
        
        public void SetActiveScoreHUD(bool isTrue) => scoreText.gameObject.SetActive(isTrue);

        public void SetActiveBoostceilBar(bool isTrue) => boostceilBar.gameObject.SetActive(isTrue);
        
        public void TotalScoreResult(object sender, GameManager.ScoreEventArgs e)
        {
            int targetScore = e.Score;
            StartCoroutine(IncreaseScore(targetScore));
        }

        private IEnumerator IncreaseScore(int targetScore)
        {
            float elapsedTime = 0f;
            int startScore = 0;
    
            while (currentScore < targetScore)
            {
                elapsedTime += Time.deltaTime;
                currentScore = Mathf.RoundToInt(Mathf.Lerp(startScore, targetScore, elapsedTime / scoreIncreaseDuration));
                totalScoreText.text = $"Score: {currentScore.ToString()}";
                yield return null;
            }
            
            AudioSpawner.Instance.ScoreRaiseSound(false);
        }
        
        public void UpdatingScore(object sender, GameManager.ScoreEventArgs e)
        {
            if (scoreText == null) return;
            this.scoreText.text = e.Score.ToString("D6");
        }
        
        public void FadeOutAnimation() => this.animator?.SetTrigger(IS_FADE_OUT);
    }
}