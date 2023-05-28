using System.Collections.Generic;
using DefaultNamespace.Resources.Item;
using UnityEngine;

public class ItemDropSpawner : SpawnerPooling
{
    private static ItemDropSpawner instance;
    public static ItemDropSpawner Instance => instance;
    private System.Random random;

    protected override void Awake()
    {
        base.Awake();
        random = new System.Random();
        if(ItemDropSpawner.instance != null) 
            Debug.LogError("There is more than one ItemDropSpawner instance");
        ItemDropSpawner.instance = this;
    }

    public virtual void Drop(List<DropRate> dropList, Vector3 pos)
    {
        foreach (DropRate dropRate in dropList)
        {
            int numItemsToSpawn = random.Next(dropRate.minDrop, dropRate.maxDrop + 1);
            for (int i = 0; i < numItemsToSpawn; i++)
            {
                if (random.Next(100) < dropRate.dropRate)
                {
                    Transform itemDrop = this.Spawn(dropRate.itemPrefab.name);
                    itemDrop.position = this.RandomizePosition(pos);
                    itemDrop.gameObject.SetActive(true);
                }
            }
        }
    }
    
    private Vector3 RandomizePosition(Vector3 originPosition)
    {
        float offsetX = Random.Range(-1f, 1f);
        float offsetY = Random.Range(0f, 1f);
        return originPosition + new Vector3(offsetX, offsetY, 0);
    }
}
