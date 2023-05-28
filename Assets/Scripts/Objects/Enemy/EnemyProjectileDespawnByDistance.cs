using Objects.Enemy.AttackEnemy;
using UnityEngine;

namespace Despawn
{
    public class EnemyProjectileDespawnByDistance : DespawnByDistanceCamera
    {
        protected override void DespawnObject()
        {
            base.DespawnObject();
            EnemyProjectileSpawner.Instance.Despawn(transform.parent);
        }
    }
}