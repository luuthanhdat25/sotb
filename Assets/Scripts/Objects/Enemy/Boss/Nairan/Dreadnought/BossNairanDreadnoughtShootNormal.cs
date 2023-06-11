using Enemy.Boss;
using Objects.Enemy.AttackEnemy;
using UnityEngine;

namespace Objects.Enemy.Boss.Nairan.Dreadnought
{
    public class BossNairanDreadnoughtShootNormal : AbsBossShoot
    {
        protected override Transform GetProjectile()
            => EnemyProjectileSpawner.Instance.Spawn(EnemyProjectileSpawner.Instance.projectile1);

        public void SetFiringRate(float rate) => this.firingRate = rate;
    }
}