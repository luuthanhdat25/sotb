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
        [SerializeField] private float timeFadeOut = 5f; 

        protected override void Awake()
        {
            base.Awake();
            if(MenuSceneManager.Instance != null) Debug.LogError("Only one MenuSceneManager allowed");
            instance = this;
        }

        public void FadeOutScene() => StartCoroutine(FadeOutSceneCoroutine());

        private IEnumerator FadeOutSceneCoroutine()
        {
            MenuUI.Instance.SetCanPlayUISFX(false);
            MenuUI.Instance?.FadeOutAnimation();
            MenuAudioManager.Instance?.FadeOutMusic(this.timeFadeOut);
            backgroundScroller?.FadeOutBackground(this.timeFadeOut);
            yield return new WaitForSeconds(this.timeFadeOut);
            this.NextSceneIndex();
        }
    }
}
