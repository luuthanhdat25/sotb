using Objects.Enemy.AttackEnemy;
using UnityEngine;

public class BossNautolanBomShoot : MinibossNautolanShoot
{
    protected override Transform GetProjectile() => EnemyProjectileSpawner.Instance.Spawn(EnemyProjectileSpawner.Instance.projectile5);
}
