using Damage.RhythmScripts;
using UnityEngine;

public class EnemySpawner : RepeatMonoBehaviour
{
    [SerializeField] private WavePrefabManager wavePrefabManager;
    
    [Header("Current")] 
    [SerializeField] private Transform currentWave;
    [SerializeField] private float wavesCurrentStartTime;
    
    private int currentWaveIndex = 0;
    private float timer = 0;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWavePrefabsManager();
    }
    
    private void LoadWavePrefabsManager() 
        => this.wavePrefabManager ??= transform.GetComponentInChildren<WavePrefabManager>();

    void Start()
    {
        this.PlayStartAudioSource();
        this.SetCurrentWave();
    }

    private void PlayStartAudioSource() => AudioManager.Instance.CurrentSoundTrack.Play();
    
    private void SetCurrentWave() => currentWave = this.wavePrefabManager.GetWaveByIndex(currentWaveIndex);
    //-----------------------------------------------------------------------------------------//
    void FixedUpdate()
    {
        if (IsAllWaveSpawn()) return;
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

    private bool IsAllWaveSpawn() => currentWaveIndex >= wavePrefabManager.GetWaveSpawnersList().Count;

    public void SpawnWaveSpawner() => currentWave.gameObject.SetActive(true);
}
