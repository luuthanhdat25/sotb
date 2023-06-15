using System;
using System.Collections;
using Damage.RhythmScripts;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Objects.UI.Level_1
{
    public class LevelOneSceneManager : RepeatSceneManager
    {
        [SerializeField] private AudioSourcesManager audioSourcesManager;
        [SerializeField] private bool isPause = false;
        [SerializeField] private Transform totalAppUI;
        [SerializeField] private Transform appButtonUI;
        [SerializeField] private Transform playerHud;
        [SerializeField] private Transform score;

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
        //[SerializeField] private Transform nextLevelButtonTransform;
        [SerializeField] private Button nextLevelButton;

        private bool isWinOrLoss = false;
        private bool isEntered = false;
        private void Start()
        {
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

        private void SetActiveMenuAndReselect(Transform menuOff, Transform menuOn, Button button)
        {
            menuOff?.gameObject.SetActive(false);
            menuOn?.gameObject.SetActive(true);
            button.Select();
        }
        
        private void NoRestartButton() => NoButtonReturnToTotalUI(restartMenu, restartButton);

        private void NoButtonReturnToTotalUI(Transform buttonMenu, Button buttonSelect)
        {
            buttonMenu?.gameObject.SetActive(false);

            if (isPause) pauseUIContent?.gameObject.SetActive(true);
            else score?.gameObject.SetActive(true);

            buttonSelect?.Select();
            appButtonUI?.gameObject.SetActive(true);
        }
        
        private void LoadComback()
        {
            combackButton?.onClick.AddListener(() => SetActiveMenuAndReselect(pauseUIContent, combackMenu, noCombackButton));
            combackButton?.onClick.AddListener(() => SetActiveMenuAndReselect(appButtonUI, combackMenu, noCombackButton));
            combackButton?.onClick.AddListener(() => SetActiveMenuAndReselect(score, combackMenu, noCombackButton));
            noCombackButton?.onClick.AddListener(NoCombackButton);
        }
        
        private void NoCombackButton() => NoButtonReturnToTotalUI(combackMenu, combackButton);

        private void LoadQuit()
        {
            quitButton?.onClick.AddListener(() => SetActiveMenuAndReselect(pauseUIContent, quitMenu, noQuitButton));
            quitButton?.onClick.AddListener(() => SetActiveMenuAndReselect(appButtonUI, quitMenu, noQuitButton));
            quitButton?.onClick.AddListener(() => SetActiveMenuAndReselect(score, quitMenu, noQuitButton));
            noQuitButton?.onClick.AddListener(NoQuitButton);
        }
        
        private void NoQuitButton() => NoButtonReturnToTotalUI(quitMenu, quitButton);

        private void Update() => CheckPauseOrContinue();

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
            isPause = true;
            continueButton?.Select();
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

        private void StopGame()
        {
            Time.timeScale = 0;
            audioSourcesManager.GetAudioSourceByIndex(0).Pause();
        }

        public void Continue()
        {
            isPause = false;
            Time.timeScale = 1;
            totalAppUI?.gameObject.SetActive(false);
            appButtonUI?.gameObject.SetActive(false);
            pauseUIContent?.gameObject.SetActive(false);
            audioSourcesManager.GetAudioSourceByIndex(0).Play();
        }

        public override void ReloadScene()
        {
            base.ReloadScene();
            Continue();
        }
        
        public override void WinGame()
        {
            isWinOrLoss = true;
            playerHud?.gameObject.SetActive(false);
            pauseUIContent?.gameObject.SetActive(false);
            appButtonUI?.gameObject.SetActive(false);

            totalAppUI?.gameObject.SetActive(true);
            score?.gameObject.SetActive(true);
            StartCoroutine(NextLevelButtonActive());
        }

        private IEnumerator NextLevelButtonActive()
        {
            yield return new WaitForSeconds(3f);
            nextLevelUIContent?.gameObject.SetActive(true);
            nextLevelButton?.Select();
            StopGame();
        }

        public override void LossGame()
        {
            isWinOrLoss = true;
            restartButton?.Select();
            pauseUIContent?.gameObject.SetActive(false);
            playerHud?.gameObject.SetActive(false);
            
            totalAppUI?.gameObject.SetActive(true);
            score?.gameObject.SetActive(true);
            appButtonUI?.gameObject.SetActive(true);
            
            Invoke("StopGame", 2);
            //Delay after time and stop game
        }

        public override void NextSceneIndex()
        {
            base.NextSceneIndex();
            Continue();
        }
        
        public override void CombackToMenu()
        {
            base.CombackToMenu();
            Continue();
        }
    }
}