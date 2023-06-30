using System.Collections;
using DefaultNamespace.Components.Audio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Objects.UI.Menu_Scene
{
    public class MenuUI : MonoBehaviour
    {
        private static MenuUI instance;
        public static MenuUI Instance => instance;
        [SerializeField] private GameObject gameTitle;
        [SerializeField] private TMP_Text gameTitleText;
        [SerializeField] private Transform totalUI;
        [SerializeField] private Image whiteBackground;
        
        [Header("Play")]
        [SerializeField] private Button playGameButton;
        [SerializeField] private TMP_Text playGameText;
        
        [Header("Credit")]
        [SerializeField] private Button creditButton;
        [SerializeField] private Transform creditMenu;
        [SerializeField] private Button outCreditButton;
        [SerializeField] private TMP_Text creditGameText;
        
        [Header("Exit")]
        [SerializeField] private Button quitGameButton;
        [SerializeField] private Transform quitMenu;
        [SerializeField] private Button yesExitGameButton;
        [SerializeField] private Button noExitGameButton;
        [SerializeField] private TMP_Text quiteGameText;
        
        private bool isDirectionButtonEntered = false;
        private bool canPlayUISFX = true;

        private void Awake()
        {
            if(MenuUI.Instance != null) Debug.LogError("Only one MenuUI allowed");
            instance = this;
        }
        
        //LoadComponent
        private void Start()
        {
            LoadPlayButton();
            LoadCreditButton();
            LoadOutCreditButton();
            LoadQuitGameButton();
            LoadQuitMenu();
        }
        
        private void LoadPlayButton()
        {
            playGameButton?.onClick.AddListener(() => MenuSceneManager.Instance.FadeOutScene());
            playGameButton?.Select();
        }

        private void LoadCreditButton()
        {
            creditButton?.onClick.AddListener(() => SetActiveTransfrom(creditMenu, true));
            creditButton?.onClick.AddListener(CreditButtonTurnOffTotalUI);
        }
        
        private void LoadOutCreditButton()
        {
            outCreditButton?.onClick.AddListener(() => SetActiveTransfrom(creditMenu, false));
            outCreditButton?.onClick.AddListener(CreditButtonTurnOnTotalUI);
        }
        
        private void LoadQuitGameButton()
        {
            quitGameButton?.onClick.AddListener(() => SetActiveTransfrom(quitMenu, true));
            quitGameButton?.onClick.AddListener(QuitButtonTurnOffTotalUI);
        }
        
        private void LoadQuitMenu()
        {
            yesExitGameButton?.onClick.AddListener(MenuSceneManager.Instance.QuitGame);
            noExitGameButton?.onClick.AddListener(() => SetActiveTransfrom(quitMenu, false));
            noExitGameButton?.onClick.AddListener(QuitButtonTurnOnTotalUI);
        }

        private void CreditButtonTurnOffTotalUI()
        {
            SetActiveTransfrom(totalUI, false);
            outCreditButton.Select();
            PlayUIEffect();
        }
        
        private void CreditButtonTurnOnTotalUI()
        {
            SetActiveTransfrom(totalUI, true);
            creditButton.Select();
            PlayUIEffect();
        }
        
        private void QuitButtonTurnOffTotalUI()
        {
            SetActiveTransfrom(totalUI, false);
            noExitGameButton.Select();
            PlayUIEffect();
        }
        
        private void QuitButtonTurnOnTotalUI()
        {
            SetActiveTransfrom(totalUI, true);
            playGameButton.Select();
            PlayUIEffect();
        }

        private void SetActiveTransfrom(Transform transform, bool isOn) 
            => transform?.gameObject.SetActive(isOn);

        private void Update() => ButtonSelectedAudio();
        
        private void ButtonSelectedAudio()
        {
            if (!canPlayUISFX) return;
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

        private void PlayUIEffect() => MenuAudioManager.Instance?.UIEffect();

        public void FadeOutWhiteBackground(float timeFadeOut)
        {
            whiteBackground.gameObject.SetActive(true);
            StartCoroutine(FadeOutWhiteBackgroundCoroutine(timeFadeOut));
        }
        
        public void FadeOutText(float timeFadeOut) => StartCoroutine(FadeOutButtonCoroutine(timeFadeOut));


        private IEnumerator FadeOutWhiteBackgroundCoroutine(float timeFadeOut)
        {
            Color startColor = whiteBackground.color;
            Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

            float timer = 0f;
            while (timer < timeFadeOut)
            {
                timer += Time.deltaTime;
                whiteBackground.color = Color.Lerp(startColor, targetColor, timer / timeFadeOut);
                yield return null;
            }
        }
        
        private IEnumerator FadeOutButtonCoroutine(float timeFadeOut)
        {
            Color startButtonColor = playGameText.color;
            Color targetButtonColor = new Color(startButtonColor.a, startButtonColor.g, startButtonColor.b, 0);

            float timer = 0f;
            while (timer < timeFadeOut)
            {
                timer += Time.deltaTime;
                float t = timer / timeFadeOut;
                gameTitleText.color = playGameText.color = creditGameText.color = quiteGameText.color = Color.Lerp(startButtonColor, targetButtonColor, timer / timeFadeOut);
                yield return null;
            }
        }

        public void SetActiveGameTitle(bool isOn) => this.gameTitle?.SetActive(isOn);
        
        public void SetActiveTotalUI(bool isOn) => this.totalUI?.gameObject.SetActive(isOn);
        
        public void SetCanPlayUISFX(bool isTrue) => this.canPlayUISFX = isTrue;
    }
}