using System.Collections;
using Damage.RhythmScripts;
using DefaultNamespace;
using DefaultNamespace.Objects.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Objects.UI.HUD
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [SerializeField] private LevelInGameSceneManager levelInGameSceneManager;
        
        [Header("Bar")]
        [SerializeField] private Transform energiesBar;
        [SerializeField] private Transform boostceilBar;
        
        [Header("Score")]
        [SerializeField] private TMP_Text totalScoreText;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text timerText;
        
        [Header("BigUI")]
        [SerializeField] private Transform totalAppUI;
        [SerializeField] private Transform appButtonUI;
        [SerializeField] private Transform playerHud;
        
        [Header("Pause UI")]
        [SerializeField] private Transform pauseUIContent;
        
        [Header("Continue")]
        [SerializeField] private Button continueButton;
        
        [Header("Restart")]
        [SerializeField] private Button restartButton;
        [SerializeField] private Transform restartMenu;
        [SerializeField] private Button noRestarButton;
        
        [Header("Comback to menu")]
        [SerializeField] private Button combackButton;
        [SerializeField] private Transform combackMenu;
        [SerializeField] private Button noCombackButton;

        [Header("Quit Game")]
        [SerializeField] private Button quitButton;
        [SerializeField] private Transform quitMenu;
        [SerializeField] private Button noQuitButton;
        
        [Header("Next Level UI")]
        [SerializeField] private Transform nextLevelUIContent;
        [SerializeField] private Button nextLevelButton;

        [Header("Win Game UI")]
        [SerializeField] private Transform winGameUIContent;
        [SerializeField] private TMP_Text timerFinishedGame;
        
        [Header("Else")]
        [SerializeField] private float scoreIncreaseDuration = 1.0f;
        private Animator animator;
        private const string IS_FADE_OUT = "isFadeOut";

        private bool isWinOrLoss = false;
        private bool isDirectionButtonEntered = false;
        private bool canPlayUISFX = true;
        private bool isEscapeButtonEntered = false;
        
        private void Awake()
        {
            if(Instance != null) Debug.LogError("There is more than one MiniBossKla_edCtrl instance");
            Instance = this;
        }
        
        private void Start()
        {
            GameManager.Instance.ScoreChangedEvent += OnUpdatingScore; 
            GameManager.Instance.ScoreResultsEvent += OnTotalScoreResultEvent;
            GameManager.Instance.TimeResultsEvent += OnUpdateWinTimer;
            this.LoadAnimator();
            this.LoadRestart();
            this.LoadComback();
            this.LoadQuit();
        }
        
        private void LoadAnimator()
        {
            if(this.animator != null) return;
            this.animator = GetComponent<Animator>();
        }

        private void LoadRestart()
        {
            restartButton?.onClick.AddListener(() => SetActiveMenuAndReselect(pauseUIContent, restartMenu, noRestarButton));
            restartButton?.onClick.AddListener(() => SetActiveMenuAndReselect(appButtonUI, restartMenu, noRestarButton));
            restartButton?.onClick.AddListener(() => SetActiveMenuAndReselect(totalScoreText.transform, restartMenu, noRestarButton));
            noRestarButton?.onClick.AddListener(NoRestartButton);
        }

        private void SetActiveMenuAndReselect(Transform menuOff, Transform menuOn, Button button)
        {
            menuOff?.gameObject.SetActive(false);
            menuOn?.gameObject.SetActive(true);
            button.Select();
            PlayUIEffect();
        }
        
        private void NoRestartButton() => NoButtonReturnToTotalUI(restartMenu, restartButton);

        private void NoButtonReturnToTotalUI(Transform buttonMenu, Button buttonSelect)
        {
            buttonMenu?.gameObject.SetActive(false);

            if (levelInGameSceneManager.GetIsPaused() && !isWinOrLoss) pauseUIContent?.gameObject.SetActive(true);
            else totalScoreText?.gameObject.SetActive(true);

            buttonSelect?.Select();
            PlayUIEffect();
            appButtonUI?.gameObject.SetActive(true);
        }
        
        private void LoadComback()
        {
            combackButton?.onClick.AddListener(() => SetActiveMenuAndReselect(pauseUIContent, combackMenu, noCombackButton));
            combackButton?.onClick.AddListener(() => SetActiveMenuAndReselect(appButtonUI, combackMenu, noCombackButton));
            combackButton?.onClick.AddListener(() => SetActiveMenuAndReselect(totalScoreText.transform, combackMenu, noCombackButton));
            noCombackButton?.onClick.AddListener(NoCombackButton);
        }
        
        private void NoCombackButton() => NoButtonReturnToTotalUI(combackMenu, combackButton);

        private void LoadQuit()
        {
            quitButton?.onClick.AddListener(() => SetActiveMenuAndReselect(pauseUIContent, quitMenu, noQuitButton));
            quitButton?.onClick.AddListener(() => SetActiveMenuAndReselect(appButtonUI, quitMenu, noQuitButton));
            quitButton?.onClick.AddListener(() => SetActiveMenuAndReselect(totalScoreText.transform, quitMenu, noQuitButton));
            noQuitButton?.onClick.AddListener(NoQuitButton);
        }
        
        private void NoQuitButton() => NoButtonReturnToTotalUI(quitMenu, quitButton);

        private void PlayUIEffect() => AudioSpawner.Instance.UIEffect();

        public void OnTotalScoreResultEvent(object sender, GameManager.ScoreEventArgs e)
        {
            int targetScore = e.Score;
            StartCoroutine(IncreaseScoreFromZero(targetScore));
        }
        
        public void OnUpdatingScore(object sender, GameManager.ScoreEventArgs e)
        {
            if (scoreText == null) return;
            this.scoreText.text = e.Score.ToString("D6");
        }
        
        private void Update()
        {
            ButtonSelectedAudio();
            CheckPauseOrContinue();
        }
        
        private void ButtonSelectedAudio()
        {
            if (!canPlayUISFX) return;
            if (!levelInGameSceneManager.GetIsPaused()) return;
            if (GameInput.Instance.GetRawInputNormalized() != Vector2.zero && !isDirectionButtonEntered)
            {
                isDirectionButtonEntered = true;
            }
            else if (GameInput.Instance.GetRawInputNormalized() == Vector2.zero && isDirectionButtonEntered)
            {
                PlayUIEffect();
                isDirectionButtonEntered = false;
            }
        }

        private void CheckPauseOrContinue()
        {
            if (isWinOrLoss) return;
            if (GameInput.Instance.IsEscapePressed() && !isEscapeButtonEntered)
            {
                isEscapeButtonEntered = true;
            }
            else if (!GameInput.Instance.IsEscapePressed() && isEscapeButtonEntered)
            {
                if (!levelInGameSceneManager.GetIsPaused()) levelInGameSceneManager.PauseScene();
                else
                {
                    if(restartMenu.gameObject.activeSelf) NoRestartButton();
                    else if(combackMenu.gameObject.activeSelf) NoCombackButton();
                    else if(quitMenu.gameObject.activeSelf) NoQuitButton();
                    else levelInGameSceneManager.ContinueScene();
                }
                isEscapeButtonEntered = false;
            }
        }
        
        private IEnumerator IncreaseScoreFromZero(int targetScore)
        {
            float elapsedTime = 0f;
            int currentScore = 0;
    
            while (currentScore < targetScore)
            {
                elapsedTime += Time.deltaTime;
                currentScore = Mathf.RoundToInt(Mathf.Lerp(0, targetScore, elapsedTime / scoreIncreaseDuration));
                totalScoreText.text = $"Score: {currentScore.ToString()}";
                yield return null;
            }
            
            AudioSpawner.Instance.ScoreRaiseSound(false);
        }
        
        public void UpdateTimerUI(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        
        public void PauseUI()
        {
            continueButton?.Select();
            PlayUIEffect();
            TurnOffAllSmallMenu();
            
            totalAppUI?.gameObject.SetActive(true);
            pauseUIContent?.gameObject.SetActive(true);
            appButtonUI?.gameObject.SetActive(true);
        }
        
        private void TurnOffAllSmallMenu()
        {
            restartMenu?.gameObject.SetActive(false);
            combackMenu?.gameObject.SetActive(false);
            quitMenu?.gameObject.SetActive(false);
        }

        public void TurnOffTotalUI()
        {
            totalAppUI?.gameObject.SetActive(false);
            appButtonUI?.gameObject.SetActive(false);
            pauseUIContent?.gameObject.SetActive(false);
        }

        public void WinLevelUI()
        {
            isWinOrLoss = true;
            playerHud?.gameObject.SetActive(false);
            //pauseUIContent?.gameObject.SetActive(false);

            totalAppUI?.gameObject.SetActive(true);
            appButtonUI?.gameObject.SetActive(false);
            
            totalScoreText?.gameObject.SetActive(true);
            StartCoroutine(NextLevelButtonActive());
        }
        
        private IEnumerator NextLevelButtonActive()
        {
            yield return new WaitForSeconds(3f);
            nextLevelUIContent?.gameObject.SetActive(true);
            nextLevelButton?.Select();
            PlayUIEffect();
            levelInGameSceneManager.StopGame();
        }

        public void LossGameUI()
        {
            isWinOrLoss = true;
            restartButton?.Select();
            pauseUIContent?.gameObject.SetActive(false);
            playerHud?.gameObject.SetActive(false);
            
            totalAppUI?.gameObject.SetActive(true);
            totalScoreText?.gameObject.SetActive(true);
            appButtonUI?.gameObject.SetActive(true);
        }

        public void FadeOutUI()
        {
            canPlayUISFX = false;
            FadeOutAnimation();
        }

        public void WinGameUI()
        {
            isWinOrLoss = true;
            playerHud?.gameObject.SetActive(false);
            pauseUIContent?.gameObject.SetActive(false);
            totalAppUI?.gameObject.SetActive(true);
            appButtonUI?.gameObject.SetActive(false);
            restartButton?.transform.gameObject.SetActive(false);
            quitButton?.transform.gameObject.SetActive(false);
            
            totalScoreText?.gameObject.SetActive(true);
            StartCoroutine(Congratulation());
        }

        private void OnUpdateWinTimer(object sender, GameManager.TimeFinishEventArgs args)
        {
            if (timerFinishedGame == null) return;
            float time = args.Time;
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time % 60f);
            int milliseconds = Mathf.FloorToInt((time * 100f) % 100f);
            timerFinishedGame.text = string.Format("Time: {0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        }

        private IEnumerator Congratulation()
        {
            yield return new WaitForSeconds(3f);
            timerFinishedGame?.gameObject.SetActive(true); PlayUIEffect();
            yield return new WaitForSeconds(3f);
            //AddMoreMusic
            winGameUIContent?.gameObject.SetActive(true); PlayUIEffect();
            yield return new WaitForSeconds(2f);
            appButtonUI?.gameObject.SetActive(true);
            combackButton.Select();
            PlayUIEffect();
            levelInGameSceneManager.StopGame();
        }

        private void FadeOutAnimation() => this.animator?.SetTrigger(IS_FADE_OUT);

        public void SetActiveEnergiesBar(bool isTrue) => energiesBar.gameObject.SetActive(isTrue);
        
        public void SetActiveScoreHUD(bool isTrue) => scoreText.gameObject.SetActive(isTrue);
        
        public void SetActiveTimerHUD(bool isTrue) => timerText.gameObject.SetActive(isTrue);

        public void SetActiveBoostceilBar(bool isTrue) => boostceilBar.gameObject.SetActive(isTrue);
    }
}