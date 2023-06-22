using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace Damage.RhythmScripts
{
    public class AudioSpawner : RepeatMonoBehaviour
    {
        public static AudioSpawner Instance { get; set; }
        [SerializeField] private float fadeDuration = 1f;
        [SerializeField] private BackgroundScroller backgroundScroller;
        [SerializeField] private Transform soundTrackAudioTransform;
        [SerializeField] private AudioClip sourceOne, sourceTwo;
        [SerializeField] private AudioSource currentSoundTrack;
        public AudioSource CurrentSoundTrack => currentSoundTrack;
        
        [SerializeField] private List<Transform> soundEffectPlayerList;
        [SerializeField] private List<Transform> soundEffectEnemyList;
        [SerializeField] private List<Transform> soundUIList;
        private float startVolume;
        private bool isChangeSong = false;
        public bool IsChangeSong => isChangeSong;
        public enum SoundEffectEnum
        {
            ExplosionPlayer, Health, UpgradeItem, Buff, UntiPlayer, Dash,
            ExplosionBoss, ExplosionNormalEnemy,
            Button, ScoreRaise
        }
        
        protected override void Awake()
        {
            if(Instance != null) Debug.LogError("There is more than one AudioSpawner instance");
            Instance = this;
            
            currentSoundTrack.clip = sourceOne;
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadSoundTrackAudioSource();
            this.LoadSoundEffectPlayerList();
            this.LoadSoundEffectEnemyList();
            this.LoadSoundUIList();
        }

        private void LoadSoundTrackAudioSource()
        {
            if (this.soundTrackAudioTransform != null) return;
            this.soundTrackAudioTransform = transform.Find("Soundtrack");
            this.currentSoundTrack = soundTrackAudioTransform.GetComponent<AudioSource>();
        }
        
        private void LoadAudioSources(Transform parent, List<AudioSource> list)
        {
            if (parent == null) return;
            AudioSource[] audioSources = parent.GetComponentsInChildren<AudioSource>();
            foreach (AudioSource audioSource in audioSources)
                list.Add(audioSource);
        }
        
        private void LoadSoundEffectPlayerList()
        {
            if (soundEffectPlayerList.Count != 0) return;
            Transform soundPlayerEffect = transform.Find("Player");
            LoadTransforms(soundPlayerEffect, soundEffectPlayerList);
        }
        
        private void LoadTransforms(Transform parent, List<Transform> list)
        {
            if (parent == null) return;
            foreach (Transform childTransform in parent) 
                list.Add(childTransform);
            
            this.HidePrefabs(list);
        }
        
        private void HidePrefabs(List<Transform> list)
        {
            foreach (Transform prefab in list)
                prefab.gameObject.SetActive(false);
        }
        
        private void LoadSoundEffectEnemyList()
        {
            if (soundEffectEnemyList.Count != 0) return;
            Transform soundEnemyEffect = transform.Find("Enemy");
            LoadTransforms(soundEnemyEffect, soundEffectEnemyList);
        }
        
        private void LoadSoundUIList()
        {
            if (soundUIList.Count != 0) return;
            Transform UI = transform.Find("UI");
            LoadTransforms(UI, soundUIList);
        }
        
        private void FixedUpdate()
        {
            if (!IsChangeSoundTrack()) return;

            if (!isChangeSong)
            {
                isChangeSong = true;
                Invoke("ChangeMusic", 0.5f);
            }
        }

        private bool IsChangeSoundTrack() => !isChangeSong && !currentSoundTrack.isPlaying && Time.timeScale != 0 && !GameManager.Instance.IsFinishGame();

        private void ChangeMusic()
        {
            currentSoundTrack.clip = sourceTwo;
            backgroundScroller?.ReverseBackground();
            currentSoundTrack.Play();
        }

        public void MusicFadeOut() => StartCoroutine(FadeOutCoroutine());
        
        private IEnumerator FadeOutCoroutine()
        {
            float startVolume = this.currentSoundTrack.volume;
            float timer = 0f;

            while (timer < fadeDuration)
            {
                timer += Time.fixedDeltaTime;
                float t = timer / fadeDuration;
                this.currentSoundTrack.volume = Mathf.Lerp(startVolume, 0f, t);
                yield return new WaitForFixedUpdate();
            }

            this.currentSoundTrack.volume = 0f;
            this.currentSoundTrack.Pause();
        }

        public void SpawnPlayerEffect(SoundEffectEnum effectEnum)
        {
            foreach (Transform transform in this.soundEffectPlayerList)
            {
                GetPrefabsAndSpawn(effectEnum, transform);
            }
        }

        private static void GetPrefabsAndSpawn(SoundEffectEnum effectEnum, Transform transform)
        {
            if (effectEnum.ToString().Equals(transform.name))
            {
                Transform newSFX = Instantiate(transform);
                AudioSource audioSource = newSFX.GetComponent<AudioSource>();
                newSFX.gameObject.SetActive(true);
                Destroy(newSFX.gameObject, audioSource.clip.length);
            }
        }

        public void SpawnEnemyEffect(SoundEffectEnum effectEnum)
        {
            foreach (Transform transform in this.soundEffectEnemyList)
            {
                GetPrefabsAndSpawn(effectEnum, transform);
            }
        }

        private void SetActiveTransform(Transform transform)
        {
            transform.gameObject.SetActive(true);
        }
        
        public void UIEffect()
        {
            SoundEffectEnum effectEnum = SoundEffectEnum.Button;
            foreach (Transform transform in this.soundUIList)
            {
                if (effectEnum.ToString().Equals(transform.name))
                {
                    transform.gameObject.SetActive(false);
                    transform.gameObject.SetActive(true);
                }
            }
        }
        
        public void PlayerDeadOver()
        {
            SoundEffectEnum effectEnum = SoundEffectEnum.ExplosionPlayer;
            foreach (Transform transform in this.soundEffectPlayerList)
            {
                if (effectEnum.ToString().Equals(transform.name))
                {
                    Transform newSFX = Instantiate(transform);
                    AudioSource audioSource = newSFX.GetComponent<AudioSource>();
                    audioSource.volume = 0.8f;
                    newSFX.gameObject.SetActive(true);
                    StartCoroutine(SlowdownAudio(audioSource));
                }
            }
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
                elapsedTime += Time.deltaTime;
            }
        }

        /*public bool isScoreRaise = false;
        public void SetIsScoreRaise(bool isScoreRaise) => this.isScoreRaise = isScoreRaise;
        [SerializeField] private GameObject scoreRaiseSound;
        private void Update()
        {
            if (!isScoreRaise) return;
            ScoreRaise();
        }
        public float toggleInterval = 0.2f;
        private float timer = 0f;
        public void ScoreRaise()
        {
            timer += Time.deltaTime;
            if (timer >= toggleInterval)
            {
                if(toggleInterval > 0.1) toggleInterval -= Time.deltaTime / 2;
                ToggleObject();
                timer = 0f;
            }
        }
        
        private void ToggleObject()
        {
            GameObject scoreRaise = Instantiate(scoreRaiseSound);
            scoreRaise.SetActive(true);
            Destroy(scoreRaise, 1f);
        }*/
    }
}