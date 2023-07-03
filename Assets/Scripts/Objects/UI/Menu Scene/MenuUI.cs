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
        [SerializeField] private Transform totalUI;
        [SerializeField] private Image whiteBackground;
        
        [Header("Play")]
        [SerializeField] private Button playGameButton;
        
        [Header("Credit")]
        [SerializeField] private Button creditButton;
        [SerializeField] private Transform creditMenu;
        [SerializeField] private Button outCreditButton;
        
        [Header("Exit")]
        [SerializeField] private Button quitGameButton;
        [SerializeField] private Transform quitMenu;
        [SerializeField] private Button yesExitGameButton;
        [SerializeField] private Button noExitGameButton;
        
        private bool isDirectionButtonEntered = false;
        private bool canPlayUISFX = true;
        private Animator animator;
        private const string IS_FADE_OUT = "isFadeOut";

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
            LoadAnimator();
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

        private void LoadAnimator()
        {
            if(this.animator != null) return;
            this.animator = GetComponent<Animator>();
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

        public void FadeOutAnimation() => this.animator?.SetTrigger(IS_FADE_OUT);
        
        public void SetCanPlayUISFX(bool isTrue) => this.canPlayUISFX = isTrue;
    }
}