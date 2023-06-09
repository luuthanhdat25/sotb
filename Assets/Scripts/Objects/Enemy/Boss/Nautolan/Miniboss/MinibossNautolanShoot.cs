using System.Collections;
using Comman;
using Enemy.Boss;
using Objects.Enemy.AttackEnemy;
using UnityEngine;

public class MinibossNautolanShoot : AbsBossShoot
{
    protected override Transform GetProjectile() => EnemyProjectileSpawner.Instance.Spawn(EnemyProjectileSpawner.Instance.projectile1);
}
