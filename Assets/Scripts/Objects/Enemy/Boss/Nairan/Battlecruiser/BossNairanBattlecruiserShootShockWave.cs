using Objects.Enemy.AttackEnemy;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemy.Boss.Nairan.Miniboss.Boss.Battlecruiser
{
    public class BossNairanBattlecruiserShootShockWave : AbsBossShoot
    {
        protected override Transform GetProjectile() 
            => EnemyProjectileSpawner.Instance.Spawn(EnemyProjectileSpawner.Instance.projectile4);

        public void SetFiringRate(float firingRate) => this.firingRate = firingRate;
    }
}