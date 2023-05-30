using UnityEngine;
using System.Collections.Generic;

public abstract class SpawnerPooling : RepeatMonoBehaviour
{
    [SerializeField] protected Transform prefabsManager;
    [SerializeField] protected Transform holderManager;
    [SerializeField] protected List<Transform> prefabsList;
    [SerializeField] protected List<Transform> poolObjsList;
    
    protected override void LoadComponents()
    {
        this.LoadPrefabsManager();
        this.LoadHolderManager();  
    }

    protected virtual void LoadPrefabsManager()
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
        if (this.prefabsList.Count > 0) return;
        //Found child transform
        foreach (Transform prefab in prefabsManager)
        {
            this.prefabsList.Add(prefab);
        }
        
        this.HidePrefabs();
    }
    
    protected virtual void HidePrefabs()
    {
        foreach (Transform prefab in this.prefabsList)
        {
            prefab.gameObject.SetActive(false);
        }
    }
    
    protected virtual void LoadHolderManager()
    {
        if (this.holderManager != null) return;
        this.holderManager = transform.Find("HolderManager");
    }
    
    
    //-----------------------------------------------------------------------------//
    public virtual Transform Spawn(string prefabsName)
    {
        Transform prefab = this.GetPrefabByName(prefabsName);
        if (prefab == null) {
            Debug.LogWarning("Prefab not found: " + prefabsName);
            return null;
        }
        
        Transform newPrefab = GetObjectFromPool(prefab);
        newPrefab.parent = this.holderManager;
        return newPrefab;
    }
    
    public virtual Transform GetPrefabByName(string prefabsName)
    {
        foreach (Transform prefab in this.prefabsList)
            if (prefab.name == prefabsName) return prefab;
        
        return null;
    }

    protected virtual Transform GetObjectFromPool(Transform prefab)
    {
        foreach (Transform objectFromPool in poolObjsList)
        {
            if (prefab.name == objectFromPool.name && !objectFromPool.gameObject.activeSelf)
            {
                this.poolObjsList.Remove(objectFromPool);
                return objectFromPool;
            }
        }

        Transform newPrefab = Instantiate(prefab);
        newPrefab.name = prefab.name;
        return newPrefab;
    }
    

    // This method sometime conflit with method ResetValue, carefully!
    public virtual void Despawn(Transform obj)
    {
        this.poolObjsList.Add(obj);
        obj.gameObject.SetActive(false);
    }
}
