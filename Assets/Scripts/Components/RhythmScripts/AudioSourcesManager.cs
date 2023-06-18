using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Damage.RhythmScripts
{
    public class AudioSourcesManager : RepeatMonoBehaviour
    {
        [SerializeField] private List<AudioSource> audioSourcesList;
        [SerializeField] private float fadeDuration = 1f;
        private float startVolume;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadAudioSourceList();
        }

        private void LoadAudioSourceList()
        {
            if (this.audioSourcesList.Count != 0) return;
            this.audioSourcesList.AddRange(GetComponentsInChildren<AudioSource>());
        }
        
        public void MusicFadeOut() => StartCoroutine(FadeOutCoroutine());
        
        private IEnumerator FadeOutCoroutine()
        {
            float startVolume = this.audioSourcesList[0].volume;
            float timer = 0f;

            while (timer < fadeDuration)
            {
                timer += Time.fixedDeltaTime;
                float t = timer / fadeDuration;
                this.audioSourcesList[0].volume = Mathf.Lerp(startVolume, 0f, t);
                yield return new WaitForFixedUpdate();
            }

            this.audioSourcesList[0].volume = 0f;
            this.audioSourcesList[0].Pause();
        }
        
        public AudioSource GetAudioSourceByIndex(int index)
        {
            if (IsAudioSourceListNull()) return null;
            return this.audioSourcesList[index];
        }

        public bool IsAudioSourceListNull()
        {
            if (this.audioSourcesList.Count != 0) return false;
            Debug.Log("Audio List null");
            return true;
        }
    }
}