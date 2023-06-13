using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Objects.UI.Menu_Scene
{
    public class MenuSceneManager : RepeatSceneManager
    {
        [SerializeField] private Transform totalUI;
        
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
            playGameButton?.onClick.AddListener(NextSceneIndex);
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
            yesExitGameButton?.onClick.AddListener(QuitGame);
            noExitGameButton?.onClick.AddListener(() => SetActiveTransfrom(quitMenu, false));
            noExitGameButton?.onClick.AddListener(QuitButtonTurnOnTotalUI);
        }

        private void CreditButtonTurnOffTotalUI()
        {
            SetActiveTransfrom(totalUI, false);
            outCreditButton.Select();
        }
        
        private void CreditButtonTurnOnTotalUI()
        {
            SetActiveTransfrom(totalUI, true);
            playGameButton.Select();
        }
        
        private void QuitButtonTurnOffTotalUI()
        {
            SetActiveTransfrom(totalUI, false);
            noExitGameButton.Select();
        }
        
        private void QuitButtonTurnOnTotalUI()
        {
            SetActiveTransfrom(totalUI, true);
            playGameButton.Select();
        }

        private void SetActiveTransfrom(Transform transform, bool isOn) 
            => transform?.gameObject.SetActive(isOn);
    }
}
