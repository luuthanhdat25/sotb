using Damage.RhythmScripts;
using UnityEngine;

public class EnemySpawner : RepeatMonoBehaviour
{
    [SerializeField] private WavePrefabsManager wavePrefabsManager;
    
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
        => this.wavePrefabsManager ??= transform.GetComponentInChildren<WavePrefabsManager>();

    void Start()
    {
        this.PlayStartAudioSource();
        this.SetCurrentWave();
    }

    private void PlayStartAudioSource() => AudioManager.Instance.CurrentSoundTrack.Play();
    
    private void SetCurrentWave() => currentWave = this.wavePrefabsManager.GetWaveByIndex(currentWaveIndex);
    //-----------------------------------------------------------------------------------------//
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (!IsAllWaveSpawn())
        {
            this.SetCurrentWave();
            if (currentWave.TryGetComponent<WaveSpawner>(out WaveSpawner waveCurrentSpawn))
            {
                wavesCurrentStartTime = waveCurrentSpawn.GetSpawnStartTime();
            }
        
            if (timer > wavesCurrentStartTime)
            {
                SpawnWaveSpawner();
                currentWaveIndex++;
                Debug.Log(currentWaveIndex);
            }
        }
        else
        {
            AudioManager.Instance.CheckPlayNextSoundTrack(timer);   
        }
    }

    private bool IsAllWaveSpawn() => currentWaveIndex >= wavePrefabsManager.GetWaveSpawnersList().Count;

    public void SpawnWaveSpawner() => currentWave.gameObject.SetActive(true);
}
