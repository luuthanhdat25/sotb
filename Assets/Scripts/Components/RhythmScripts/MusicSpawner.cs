using System.Collections.Generic;
using System.Net.NetworkInformation;
using Damage.RhythmScripts;
using UnityEngine;

public class MusicSpawner : RepeatMonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSourcesManager audioSourcesManager;
    [SerializeField] private WavePrefabsManager wavePrefabsManager;
    
    [Header("Current")] 
    private int currentAudioSourceIndex = 0;
    [SerializeField] private Transform currentWave;
    [SerializeField] private float wavesCurrentStartTime;
    
    private int currentWaveIndex = 0;
    private float timer = 0;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAudioSourcesManager();
        this.LoadWavePrefabsManager();
    }

    private void LoadAudioSourcesManager()
    {
        if (this.audioSourcesManager != null) return;
        this.audioSourcesManager = transform.GetComponentInChildren<AudioSourcesManager>();
    }

    private void LoadWavePrefabsManager()
    {
        if (this.wavePrefabsManager != null) return;
        this.wavePrefabsManager = transform.GetComponentInChildren<WavePrefabsManager>();
    }
    
    //-----------------------------------------------------------------------------------------//
    void Start()
    {
        this.PlayCurrentAudioSource();
        this.SetCurrentWave();
    }

    private void PlayCurrentAudioSource() => this.audioSourcesManager.GetAudioSourceByIndex(currentAudioSourceIndex).Play();
    
    
    private void SetCurrentWave() => currentWave = this.wavePrefabsManager.GetWaveByIndex(currentWaveIndex);
    //-----------------------------------------------------------------------------------------//
    void FixedUpdate()
    {
        if (currentWaveIndex >= wavePrefabsManager.GetWaveSpawnersList().Count) return;
        this.SetCurrentWave();
        if (currentWave.TryGetComponent<WaveSpawner>(out WaveSpawner waveCurrentSpawn))
        {
            wavesCurrentStartTime = waveCurrentSpawn.GetSpawnStartTime();
        }
        timer += Time.fixedDeltaTime;
        if (timer > wavesCurrentStartTime)
        {
            SpawnWaveSpawner();
            currentWaveIndex++;
            Debug.Log(currentWaveIndex);
        }
    }

    public void SpawnWaveSpawner()
    {
        currentWave.gameObject.SetActive(true);
    }
}
