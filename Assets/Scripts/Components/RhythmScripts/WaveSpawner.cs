using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Damage.RhythmScripts
{
    public class WaveSpawner : RepeatMonoBehaviour
    {
        [SerializeField] private float spawnStartTime;
        [SerializeField] private float spawnRate = 0.5f;
        [SerializeField] private float enemyMoveSpeed = 5f;
        [Header("Loader")]
        [SerializeField] private Transform pathPrefabs;
        [SerializeField] private Transform prefabsManager;
        
        [SerializeField] private List<Transform> enemyPrefabsList;
        public List<Transform> pathWayPoints { get; private set; }
        
        protected override void LoadComponents()
        {
            this.LoadPrefabsManager();
            this.LoadPathWayPoints();
        }
        
        private void LoadPrefabsManager()
        {
            if (this.prefabsManager != null) this.LoadPrefabs();
            else
            {
                this.prefabsManager = transform.Find("PrefabsManager");
                this.LoadPrefabs();    
            }
        }
        
        protected virtual void LoadPrefabs()
        {
            if (this.enemyPrefabsList.Count > 0) return;
            foreach (Transform prefab in prefabsManager)
                this.enemyPrefabsList.Add(prefab);
            
            this.HidePrefabs();
        }
    
        protected virtual void HidePrefabs()
        {
            foreach (Transform prefab in this.enemyPrefabsList)
                prefab.gameObject.SetActive(false);
        }
        
        public void LoadPathWayPoints()
        {
            if (pathPrefabs == null) return;
            //Get paths from pathPrefabs
            pathWayPoints = new List<Transform>();
            foreach (Transform childTransform in pathPrefabs)
                pathWayPoints.Add(childTransform);
        }
        //-----------------------------------------------------------//
        private void Start() {
            if (this.enemyPrefabsList == null)
            {
                Debug.Log("Prefabs null");
                return;
            }
            StartCoroutine(SpawnEnemy());
        }
    
        private IEnumerator SpawnEnemy()
        {
            for (int i = 0; i < enemyPrefabsList.Count; i++)
            {
                enemyPrefabsList[i].gameObject.SetActive(true);
                yield return new WaitForSeconds(spawnRate);
            }
        }
        //-----------------------------------------------------------//

        public float GetSpawnStartTime() => this.spawnStartTime;
        
        public float GetMoveSpeed() => this.enemyMoveSpeed;
        
        public Transform GetStartingWaypoint()
        {
            if (pathPrefabs == null) return null;
            return pathPrefabs.GetChild(0);
        }
    }
}