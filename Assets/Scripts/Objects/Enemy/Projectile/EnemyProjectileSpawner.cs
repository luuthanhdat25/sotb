using System;
using UnityEngine;

namespace Objects.Enemy.AttackEnemy
{
    public class EnemyProjectileSpawner : SpawnerPooling
    {
        private static EnemyProjectileSpawner instance;
        public static EnemyProjectileSpawner Instance { get => instance; }

        public String projectile1 = "Projectile1";
        public String projectile1_Miniboss_Kla_ed1 = "Projectile1_Miniboss_Kla_ed1";
        public String projectile1_Miniboss_Kla_ed2 = "Projectile1_Miniboss_Kla_ed2";
        public String projectile2_Miniboss_Kla_ed = "Projectile2_Miniboss_Kla_ed";
        public String bom = "Bom";
        protected override void Awake()
        {
            base.Awake();
            if (EnemyProjectileSpawner.instance != null) Debug.LogError("Only 1 EnemyProjectileSpawner can exist!");
            EnemyProjectileSpawner.instance = this;
        }
    }
}