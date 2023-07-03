using System.Collections;
using Damage.RhythmScripts;
using UnityEngine;

public class EnemySpawner : RepeatMonoBehaviour
{
    [SerializeField] private float timeDelayStartWave = 0;
    [SerializeField] private WavePrefabManager wavePrefabManager;
    
    [Header("Current")] 
    [SerializeField] private Transform currentWave;
    [SerializeField] private float wavesCurrentStartTime;
    
    private int currentWaveIndex = 0;
    private float timer = 0;
    private bool isStartSpawn = false;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWavePrefabsManager();
    }
    
    private void LoadWavePrefabsManager() 
        => this.wavePrefabManager ??= transform.GetComponentInChildren<WavePrefabManager>();

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(timeDelayStartWave);
        this.PlayStartAudioSource();
        isStartSpawn = true;
    }

    private void PlayStartAudioSource() => AudioSpawner.Instance.PlayStartAudioSource();
    
    private void SetCurrentWave() => currentWave = this.wavePrefabManager.GetWaveByIndex(currentWaveIndex);
    //-----------------------------------------------------------------------------------------//
    private void FixedUpdate()
    {
        if (!isStartSpawn) return;
        if (currentWaveIndex >= wavePrefabManager.GetWaveSpawnersList().Count) return;
        
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

    public void SpawnWaveSpawner() => currentWave.gameObject.SetActive(true);
}
