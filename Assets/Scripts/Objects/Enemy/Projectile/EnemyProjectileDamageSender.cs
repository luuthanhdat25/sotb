using Damage;
using Objects.Enemy.AttackEnemy;

namespace Projectile
{
    public class EnemyProjectileDamageSender : DamageSender
    {
        public override void GotHit()
        {
            EnemyProjectileSpawner.Instance.Despawn(transform.parent);
        }
    }
}