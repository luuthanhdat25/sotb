using Damage.RhythmScripts;
using Objects.UI.HUD;
using UnityEngine;

namespace DefaultNamespace.Objects.UI
{
    public class LevelInGameSceneManager : RepeatSceneManager
    {
        [SerializeField] protected BackgroundScroller backgroundScroller;
        [SerializeField] protected float timeFadeIn = 3f;
        [SerializeField] protected float timeFadeOut = 3f;
        
        protected bool isPause = false;

        protected virtual void Start() => backgroundScroller?.FadeInBackground(timeFadeIn);
        
        public virtual void PauseScene()
        {
            isPause = true;
            StopGame();
            UIManager.Instance.PauseUI();
        }
        
        public virtual void StopGame()
        {
            Time.timeScale = 0;
            GameManager.Instance.PauseGame();
            GameManager.Instance.SetIsStopTimer(true);
            AudioSpawner.Instance.PauseCurrentSoundTrack();
        }
        
        public virtual void ContinueScene()
        {
            isPause = false;
            ContinueGame();
            UIManager.Instance.TurnOffTotalUI();
        }

        protected virtual void ContinueGame()
        {
            Time.timeScale = 1;
            GameManager.Instance.ContinueGame();
            GameManager.Instance.SetIsStopTimer(false);
            AudioSpawner.Instance.PlayCurrentSoundTrack();
        }

        public override void ReloadScene()
        {
            base.ReloadScene();
            ContinueScene();
        }
        
        public override void LossGame()
        {
            isPause = true;
            Invoke("StopGame", 2);
            UIManager.Instance.LossGameUI();
        }

        public override void NextSceneIndex()
        {
            base.NextSceneIndex();
            ContinueScene();
        }
        
        public override void CombackToMenu()
        {
            base.CombackToMenu();
            ContinueScene();
        }

        public bool GetIsPaused() => this.isPause;
    }
}