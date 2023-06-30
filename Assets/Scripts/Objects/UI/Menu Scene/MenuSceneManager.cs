using System.Collections;
using DefaultNamespace.Components.Audio;
using UnityEngine;

namespace DefaultNamespace.Objects.UI.Menu_Scene
{
    public class MenuSceneManager : RepeatSceneManager
    {
        private static MenuSceneManager instance;
        public static MenuSceneManager Instance => instance;
        [SerializeField] private BackgroundScroller backgroundScroller;
        
        [Header("Setting FadeOut")]
        [SerializeField] private float timeFadeOutText = 3f; 
        [SerializeField] private float timeFadeOutBackground = 5f; 

        protected override void Awake()
        {
            base.Awake();
            if(MenuSceneManager.Instance != null) Debug.LogError("Only one MenuSceneManager allowed");
            instance = this;
        }

        public void FadeOutScene() => StartCoroutine(FadeOutSceneCoroutine());

        private IEnumerator FadeOutSceneCoroutine()
        {
            //Turn off all UI and button sound
            //MenuUI.Instance.SetActiveGameTitle(false);
            //MenuUI.Instance.SetActiveTotalUI(false);
            MenuUI.Instance.SetCanPlayUISFX(false);
            MenuUI.Instance?.FadeOutText(this.timeFadeOutText);
            MenuAudioManager.Instance?.FadeOutMusic(this.timeFadeOutBackground + this.timeFadeOutText);
            backgroundScroller?.FadeOutBackground(this.timeFadeOutBackground);
            yield return new WaitForSeconds(this.timeFadeOutText);
            MenuUI.Instance?.FadeOutWhiteBackground(this.timeFadeOutBackground);
            yield return new WaitForSeconds(this.timeFadeOutBackground);
            //NextScene
            this.NextSceneIndex();
        }
    }
}
