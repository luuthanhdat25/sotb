using Objects.Enemy.AttackEnemy;
using UnityEngine;

public class BossNautolanShockWaveShoot : MinibossNautolanShoot
{
    protected override Transform GetProjectile() => EnemyProjectileSpawner.Instance.Spawn(EnemyProjectileSpawner.Instance.projectile2_Miniboss_Kla_ed);
}
