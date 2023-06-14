using Despawn;
using Objects.Enemy.AttackEnemy;
using UnityEngine;

namespace Enemy
{
    public class EnemyDespawnByDistance : DespawnByDistanceCamera
    {
        protected override void DespawnObject()
        {
            base.DespawnObject();
            EnemyProjectileSpawner.Instance.Despawn(transform.parent);
        }
    }
}