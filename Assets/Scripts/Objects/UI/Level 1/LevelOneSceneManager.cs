using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Objects.UI.Level_1
{
    public class LevelOneSceneManager : RepeatSceneManager
    {
        [SerializeField] private bool isPause = false;
        [SerializeField] private Transform totalAppUI;
        [SerializeField] private Transform appButtonUI;
        [SerializeField] private Transform playerHud;
        [SerializeField] private Transform score;

        [SerializeField] private Transform currentTileUI;
        
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
        [SerializeField] private Transform nextLevelButtonTransform;
        [SerializeField] private Button nextLevelButton;

        private bool isWinOrLoss = false;
        private bool isEntered = false;
        private void Start()
        {
            currentTileUI = pauseUIContent;
            LoadRestart();
            LoadComback();
            LoadQuit();
        }
        
        private void LoadRestart()
        {
            restartButton?.onClick.AddListener(() => SetActiveMenuAndReselect(pauseUIContent, restartMenu, noRestarButton));
            restartButton?.onClick.AddListener(() => SetActiveMenuAndReselect(appButtonUI, restartMenu, noRestarButton));
            restartButton?.onClick.AddListener(() => SetActiveMenuAndReselect(score, restartMenu, noRestarButton));
            noRestarButton?.onClick.AddListener(NoRestartButton);
        }

        private void NoRestartButton()
        {
            if (isPause)
            {
                SetActiveMenuAndReselect(restartMenu, pauseUIContent, restartButton);
            }
            else
            {
                SetActiveMenuAndReselect(restartMenu, score, restartButton);
            }
            
            SetActiveMenuAndReselect(restartMenu, appButtonUI, restartButton);
        }
        
        private void LoadComback()
        {
            combackButton?.onClick.AddListener(() => SetActiveMenuAndReselect(pauseUIContent, combackMenu, noCombackButton));
            combackButton?.onClick.AddListener(() => SetActiveMenuAndReselect(appButtonUI, combackMenu, noCombackButton));
            combackButton?.onClick.AddListener(() => SetActiveMenuAndReselect(score, combackMenu, noCombackButton));
            noCombackButton?.onClick.AddListener(NoCombackButton);
        }
        
        private void NoCombackButton()
        {
            if (isPause)
            {
                SetActiveMenuAndReselect(combackMenu, pauseUIContent, combackButton);
            }
            else
            {
                SetActiveMenuAndReselect(combackMenu, score, combackButton);
            }
            
            SetActiveMenuAndReselect(combackMenu, appButtonUI, combackButton);
        }
        
        private void LoadQuit()
        {
            quitButton?.onClick.AddListener(() => SetActiveMenuAndReselect(pauseUIContent, quitMenu, noQuitButton));
            quitButton?.onClick.AddListener(() => SetActiveMenuAndReselect(appButtonUI, quitMenu, noQuitButton));
            quitButton?.onClick.AddListener(() => SetActiveMenuAndReselect(score, quitMenu, noQuitButton));
            noQuitButton?.onClick.AddListener(NoQuitButton);
        }
        
        private void NoQuitButton()
        {
            if (isPause)
            {
                SetActiveMenuAndReselect(quitMenu, pauseUIContent, quitButton);
            }
            else
            {
                SetActiveMenuAndReselect(quitMenu, score, quitButton);
            }
            
            SetActiveMenuAndReselect(quitMenu, appButtonUI, quitButton);
        }
        
        private void Update()
        {
            CheckPauseOrContinue();
        }

        private void CheckPauseOrContinue()
        {
            if (isWinOrLoss) return;
            if (GameInput.Instance.IsEscapePressed() && !isEntered)
            {
                isEntered = true;
            }
            else if (!GameInput.Instance.IsEscapePressed() && isEntered)
            {
                if (!isPause) PauseGame();
                else
                {
                    if(restartMenu.gameObject.activeSelf) NoRestartButton();
                    else if(combackMenu.gameObject.activeSelf) NoCombackButton();
                    else if(quitMenu.gameObject.activeSelf) NoQuitButton();
                    else Continue();
                }
                isEntered = false;
            }
        }

        private void PauseGame()
        {
            StopGame();            
            continueButton?.Select();
            totalAppUI.gameObject.SetActive(true);
            pauseUIContent.gameObject.SetActive(true);
            appButtonUI.gameObject.SetActive(true);
            restartMenu.gameObject.SetActive(false);
            combackMenu.gameObject.SetActive(false);
            quitMenu.gameObject.SetActive(false);
            isPause = true;
        }
        
        private static void StopGame()
        {
            Time.timeScale = 0;
        }

        public void Continue()
        {
            Time.timeScale = 1;
            totalAppUI.gameObject.SetActive(false);
            appButtonUI.gameObject.SetActive(false);
            pauseUIContent.gameObject.SetActive(false);
            isPause = false;
        }

        public override void ReloadScene()
        {
            base.ReloadScene();
            Continue();
        }

        private void SetActiveMenuAndReselect(Transform menuOff, Transform menuOn, Button button)
        {
            SetActiveTransfrom(menuOff, false);
            SetActiveTransfrom(menuOn, true);
            button.Select();
        }

        public override void WinGame()
        {
            isWinOrLoss = true;
            SetActiveTransfrom(totalAppUI, true);
            SetActiveTransfrom(score, true);
            SetActiveTransfrom(playerHud, false);
            SetActiveTransfrom(pauseUIContent, false);
            SetActiveTransfrom(appButtonUI, false);
            StartCoroutine(NextLevelButtonActive());
        }

        private IEnumerator NextLevelButtonActive()
        {
            yield return new WaitForSeconds(3f);
            SetActiveTransfrom(nextLevelUIContent, true);
            nextLevelButton.Select();
            StopGame();
        }

        public override void LossGame()
        {
            isWinOrLoss = true;
            restartButton.Select();
            SetActiveTransfrom(totalAppUI, true);
            SetActiveTransfrom(score, true);
            SetActiveTransfrom(pauseUIContent, false);
            SetActiveTransfrom(appButtonUI, true);
            SetActiveTransfrom(playerHud, false);
            StopGame();
            //Delay after time and stop game
        }
        
        private void SetActiveTransfrom(Transform transform, bool isOn) 
            => transform?.gameObject.SetActive(isOn);
    }
}