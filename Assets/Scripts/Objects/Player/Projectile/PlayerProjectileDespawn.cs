using System;
using Despawn;
using UnityEngine;

public class PlayerProjectileDespawn : DespawnByDistanceCamera
{
    protected override void DespawnObject()
    {
        base.DespawnObject();
        PlayerProjectileSpawner.Instance.Despawn(transform.parent);
    }
}
