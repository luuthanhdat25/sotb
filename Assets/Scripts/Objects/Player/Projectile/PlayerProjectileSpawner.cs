using System;
using UnityEngine;

public class PlayerProjectileSpawner : SpawnerPooling
{
    private static PlayerProjectileSpawner instance;
    public static PlayerProjectileSpawner Instance { get => instance; }

    public String projectile1 = "Projectile1";
    public String projectile2 = "Projectile2";
    public String unti = "Unti";
    protected override void Awake()
    {
        base.Awake();
        if (PlayerProjectileSpawner.instance != null) Debug.LogError("Only 1 PlayerProjectileSpawner can exist!");
        PlayerProjectileSpawner.instance = this;
    }
}
