using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Components.Audio
{
    public class MenuAudioManager: MonoBehaviour
    {
        private static MenuAudioManager instance;
        public static MenuAudioManager Instance => instance;
        [SerializeField] private float fadeInDuration = 1f;
        [SerializeField] private AudioSource currentSoundTrack;
        [SerializeField] private List<AudioClip> backgroundAudioList;
        [SerializeField] private GameObject buttonSound;
        private bool isChangeSong;
        private float targetVolume;
        
        private void Awake()
        {
            if(MenuAudioManager.Instance != null) Debug.LogError("Only one MenuAudioManager allowed");
            instance = this;
        }
        
        private void Start()
        {
            targetVolume = currentSoundTrack.volume;
            currentSoundTrack.clip = GetRandomAudio();
            currentSoundTrack.Play();
            StartCoroutine(FadeIn());
        } 

        private AudioClip GetRandomAudio()
        {
            int random = Random.Range(0, backgroundAudioList.Count);
            return backgroundAudioList[random];
        }
        
        private IEnumerator FadeIn()
        {
            float elapsedTime = 0;
            float initialVolume = 0;
            while (elapsedTime < fadeInDuration)
            {
                currentSoundTrack.volume = Mathf.Lerp(initialVolume, targetVolume, elapsedTime / fadeInDuration);
                elapsedTime += UnityEngine.Time.deltaTime;
                yield return null;
            }
            currentSoundTrack.volume = targetVolume;
        }
        
        private void FixedUpdate()
        {
            if (!IsChangeSoundTrack()) return;
            isChangeSong = true;
            Invoke("ChangeMusic", 1f);
        }

        private bool IsChangeSoundTrack() => !isChangeSong && !currentSoundTrack.isPlaying;

        private void ChangeMusic()
        {
            isChangeSong = false;
            currentSoundTrack.clip = GetRandomAudio();
            currentSoundTrack.Play();
        }

        public void FadeOutMusic(float timeFadeOut) => StartCoroutine(FadeOutCoroutine(timeFadeOut));
        
        private IEnumerator FadeOutCoroutine(float timeFadeOut) 
        {
            float startVolume = this.currentSoundTrack.volume;
            float timer = 0f;

            while (timer < timeFadeOut)
            {
                timer += UnityEngine.Time.deltaTime;
                float t = timer / timeFadeOut;
                this.currentSoundTrack.volume = Mathf.Lerp(startVolume, 0f, t);
                yield return new WaitForFixedUpdate();
            }

            this.currentSoundTrack.volume = 0f;
            this.currentSoundTrack.Pause();
        }
        

        public void UIEffect()
        {
            if(buttonSound.activeSelf) buttonSound.SetActive(false);
            buttonSound.SetActive(true);
        }
        
        private IEnumerator SlowdownAudio(AudioSource audioSource)
        {
            float elapsedTime = 0f;
            float startPitch = audioSource.pitch;
            float slowdownDuration = 0.4f;
            float targetPitch = 0.15f;
            while (elapsedTime < slowdownDuration)
            {
                audioSource.pitch = Mathf.Lerp(startPitch, targetPitch, elapsedTime / slowdownDuration);
                yield return null;
                elapsedTime += UnityEngine.Time.deltaTime;
            }
        }
    }
}