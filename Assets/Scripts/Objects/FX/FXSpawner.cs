using System;
using UnityEngine;

public class FXSpawner : SpawnerPooling
{
    private static FXSpawner instance;
    public static FXSpawner Instance { get => instance; }

    public static String smoke_1 = "Smoke_1";
    public static String smoke_2 = "Smoke_2";
    public static String smoke_3 = "Smoke_3";
    public static String impact_1 = "Impact_1";
    public static String impact_2 = "Impact_2";
    public static String impact_3= "Impact_3";
    public static String untiCollision= "UntiCollision";
    public static String untiImpact= "UntiImpact";
    protected override void Awake()
    {
        base.Awake();
        if (FXSpawner.instance != null) Debug.LogError("Only 1 FXSpawner can exist!");
        FXSpawner.instance = this;
    }
}
