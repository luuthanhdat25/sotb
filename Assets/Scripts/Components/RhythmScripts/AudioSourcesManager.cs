using System.Collections.Generic;
using UnityEngine;

namespace Damage.RhythmScripts
{
    public class AudioSourcesManager : RepeatMonoBehaviour
    {
        //Load List Sources 
        [SerializeField] private List<AudioSource> audioSourcesList;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadAudioSourceList();
        }

        private void LoadAudioSourceList()
        {
            if (this.audioSourcesList.Count != 0) return;
            foreach (Transform transformHasAudioSource in this.transform)
                if (transformHasAudioSource.TryGetComponent<AudioSource>(out AudioSource audioSource))
                    this.audioSourcesList.Add(audioSource);
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

        public void StopCurrentMusic() => this.audioSourcesList[0].Pause();
    }
}