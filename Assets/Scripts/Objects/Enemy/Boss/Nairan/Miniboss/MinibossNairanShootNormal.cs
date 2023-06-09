using Objects.Enemy.AttackEnemy;
using UnityEngine;

namespace Enemy.Boss.Nairan.Miniboss
{
    public class MinibossNairanShootNormal : AbsBossShoot
    {
        protected override Transform GetProjectile() => EnemyProjectileSpawner.Instance.Spawn(EnemyProjectileSpawner.Instance.projectile1);
    }
}