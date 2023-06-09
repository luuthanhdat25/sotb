using Enemy.Boss;
using Objects.Enemy.AttackEnemy;
using UnityEngine;

public class BossNautolanShockWaveShoot : AbsBossShoot
{
    protected override Transform GetProjectile() => EnemyProjectileSpawner.Instance.Spawn(EnemyProjectileSpawner.Instance.projectile4);
}
