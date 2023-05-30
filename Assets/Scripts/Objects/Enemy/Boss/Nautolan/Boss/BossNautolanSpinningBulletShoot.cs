using Objects.Enemy.AttackEnemy;
using UnityEngine;

public class BossNautolanSpinningBulletShoot : MinibossNautolanShoot
{
    protected override Transform GetProjectile() => EnemyProjectileSpawner.Instance.Spawn(EnemyProjectileSpawner.Instance.projectile1_Miniboss_Kla_ed1);
}
