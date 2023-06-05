using Damage;
using Objects.Enemy.AttackEnemy;
using UnityEngine;

namespace Projectile
{
    public class EnemyProjectileDamageReceiver : DamageReceiver
    {
        protected override void OnDead()
        {
            EnemyProjectileSpawner.Instance.Despawn(transform.parent);
        }
    }
}