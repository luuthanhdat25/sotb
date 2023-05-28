using Despawn;
using UnityEngine;

public class FXDespawn : DespawnByTime
{
    protected override void DespawnObject()
    {
        FXSpawner.Instance.Despawn(transform.parent);
    }
}
