using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Objects.UI.Level_1
{
    public class LevelOneSceneManager : RepeatSceneManager
    {
        [SerializeField] private bool isPause = false;
        [SerializeField] private Transform background;
        
        [Header("Pause UI")]
        [SerializeField] private Transform pauseUI;
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
        [SerializeField] private Transform nextLevelUI;
        [SerializeField] private Transform nextLevelButtonTransform;
        [SerializeField] private Button nextLevelButton;

        private bool isWinOrLoss = false;
        private bool isEntered = false;
        private void Start()
        {
            continueButton?.Select();
            LoadRestart();
            LoadComback();
            LoadQuit();
        }
        
        private void LoadRestart()
        {
            restartButton?.onClick.AddListener(() => SetActiveMenuAndReselect(pauseUIContent, restartMenu, noRestarButton));
            noRestarButton?.onClick.AddListener(() => SetActiveMenuAndReselect(restartMenu, pauseUIContent, continueButton));
        }
        
        private void LoadComback()
        {
            combackButton?.onClick.AddListener(() => SetActiveMenuAndReselect(pauseUIContent, combackMenu, noCombackButton));
            noCombackButton?.onClick.AddListener(() => SetActiveMenuAndReselect(combackMenu, pauseUIContent, continueButton));
        }
        
        private void LoadQuit()
        {
            quitButton?.onClick.AddListener(() => SetActiveMenuAndReselect(pauseUIContent, quitMenu, noQuitButton));
            noQuitButton?.onClick.AddListener(() => SetActiveMenuAndReselect(quitMenu, pauseUIContent, continueButton));
        }
        
        private void Update()
        {
            if (isWinOrLoss) return;
            if (GameInput.Instance.IsEscapePressed() && !isEntered)
            {
                isEntered = true;
            }else if (!GameInput.Instance.IsEscapePressed() && isEntered)
            {
                if(!isPause) PauseGame();
                else Continue();
                isEntered = false;
            }
        }

        private void PauseGame()
        {
            Time.timeScale = 0;
            pauseUI.gameObject.SetActive(true);
            background.gameObject.SetActive(true);
            isPause = true;
        }

        public void Continue()
        {
            Time.timeScale = 1;
            pauseUI.gameObject.SetActive(false);
            background.gameObject.SetActive(false);
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

        public void NextLevelActive()
        {
            SetActiveTransfrom(nextLevelUI, true);
            SetActiveTransfrom(background, true);
            isWinOrLoss = true;
            StartCoroutine(NextLevelButtonActive());
        }

        private IEnumerator NextLevelButtonActive()
        {
            yield return new WaitForSeconds(3f);
            SetActiveTransfrom(nextLevelButtonTransform, true);
            nextLevelButton.Select();
        }
        
        private void SetActiveTransfrom(Transform transform, bool isOn) 
            => transform?.gameObject.SetActive(isOn);
    }
}