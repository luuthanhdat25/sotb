using System.Collections.Generic;
using System.Net.NetworkInformation;
using Damage.RhythmScripts;
using UnityEngine;

public class MusicSpawner : RepeatMonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private float bpmCurrent = 161;
    [SerializeField] private AudioSourcesManager audioSourcesManager;
    [SerializeField] private WavePrefabsManager wavePrefabsManager;
    
    [Header("Wave")] 

    [Header("Current")] 
    private int currentAudioSourceIndex = 0;
    [SerializeField] private Transform currentWave;
    [SerializeField] private float wavesCurrentStartTime;
    //[SerializeField] private float wavesCurrentSpawnRate;
    
    // Time of the last beat
    private float lastBeatTime = 0.0f;
    // Interval between beats (in seconds)
    private float beatInterval = 0.0f;
    // Number of beats counted so far
    public int beatCount = 0;
    private int currentWaveIndex = 0;
    // Timer for spawning objects
    public float spawnTimer = 0.0f;

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
        this.CalculateBeatInterval();
        this.SetCurrentWave();
    }

    private void PlayCurrentAudioSource() => this.audioSourcesManager.GetAudioSourceByIndex(currentAudioSourceIndex).Play();
    
    private void CalculateBeatInterval() => beatInterval = 60.0f / bpmCurrent;
    
    private void SetCurrentWave() => currentWave = this.wavePrefabsManager.GetWaveByIndex(currentWaveIndex);
    //-----------------------------------------------------------------------------------------//
    void Update()
    {
        if (currentWaveIndex > wavePrefabsManager.GetWaveSpawnersList().Count) return;
        // Calculate the time since the last beat
        float timeSinceLastBeat = Time.time - lastBeatTime;
        this.SetCurrentWave();
        // If enough time has passed since the last beat, count the beat
        if (timeSinceLastBeat > beatInterval)
        {
            // Remember the time of the last beat
            lastBeatTime = Time.time;
            beatCount++;
            //Get wave current
            if (currentWave.TryGetComponent<WaveSpawner>(out WaveSpawner waveCurrentSpawn))
            {
                wavesCurrentStartTime = waveCurrentSpawn.GetSpawnStartTime();
            }
            
            // If this is the start time of the current wave, start spawning objects
            if (beatCount == Mathf.RoundToInt(wavesCurrentStartTime / beatInterval))
            {
                spawnTimer = 0.0f;
            }

            // If this is the fourth beat of the current wave, spawn an object
            if (beatCount == Mathf.RoundToInt((wavesCurrentStartTime + spawnTimer) / beatInterval) 
                && spawnTimer < (Mathf.RoundToInt((wavesCurrentStartTime + beatInterval) / beatInterval) - Mathf.RoundToInt(wavesCurrentStartTime / beatInterval)) * beatInterval)
            {
                SpawnWaveSpawner();
            }

            // If the current wave is finished spawning objects, move to the next wave
            if (beatCount == Mathf.RoundToInt((wavesCurrentStartTime) / beatInterval)
                && currentWaveIndex < this.wavePrefabsManager.GetWaveSpawnersList().Count - 1)
            {
                
                currentWaveIndex++;
                Debug.Log(currentWaveIndex);
                spawnTimer = 0.0f;
            }
        }
        /*
         * If the music change, need to reset bpm
         */
        // Increment the spawn timer
        spawnTimer += Time.deltaTime;
    }

    public void SpawnWaveSpawner()
    {
        /*Transform newWave = Instantiate(currentWave);
        newWave.parent = this.waveHolder;
        newWave.gameObject.SetActive(true);*/
        currentWave.gameObject.SetActive(true);
    }
}
