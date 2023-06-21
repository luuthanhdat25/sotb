using System.Collections.Generic;
using UnityEngine;

namespace Damage.RhythmScripts
{
    public class WavePrefabManager : RepeatMonoBehaviour
    {
        //Load List WavePrefabs
        [SerializeField] private List<Transform> waveSpawnersList;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadWaveSpawnerList();
        }

        private void LoadWaveSpawnerList()
        {
            if(this.waveSpawnersList.Count != 0) return;
            foreach (Transform wave in this.transform)
            {
                WaveSpawner waveSpawner = wave.GetComponent<WaveSpawner>();
                if(waveSpawner != null) this.waveSpawnersList.Add(wave);
            }
            this.HidePrefabs();
        }
        
        protected virtual void HidePrefabs()
        {
            foreach (Transform prefab in this.transform)
                prefab.gameObject.SetActive(false);
        }

        public Transform GetWaveByIndex(int index)
        {
            if (IsWaveSpawnersListNull()) return null;
            return this.waveSpawnersList[index];
        }
        
        public bool IsWaveSpawnersListNull()
        {
            if (this.waveSpawnersList.Count != 0) return false;
            Debug.Log("Wave Spawner List null");
            return true;
        }

        public List<Transform> GetWaveSpawnersList() => this.waveSpawnersList;
    }
}