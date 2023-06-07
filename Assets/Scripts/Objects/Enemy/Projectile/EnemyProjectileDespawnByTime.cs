using Despawn;
using Objects.Enemy.AttackEnemy;
using UnityEngine;

namespace Objects.Enemy.Projectile
{
    public class EnemyProjectileDespawnByTime : DespawnByTime
    {
        protected override void DespawnObject()
        {
            base.DespawnObject();
            EnemyProjectileSpawner.Instance.Despawn(transform.parent);
        }
    }
}