using Objects.Enemy.AttackEnemy;
using UnityEngine;

namespace Enemy
{
    public class EnemyAttackShoot : AbsEnemyShoot
    {
        protected virtual void Start() => isFiring = true;
        
        protected override Transform GetProjectile()
        {
            return EnemyProjectileSpawner.Instance.Spawn(EnemyProjectileSpawner.Instance.projectile1);
        }
    }
}