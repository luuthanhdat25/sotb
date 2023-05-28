using Despawn;
using UnityEngine;

namespace Enemy
{
    public class EnemyDespawnByDistance : DespawnByDistanceCamera
    {
        protected override void DespawnObject()
        {
            base.DespawnObject();
            Destroy(transform.parent.gameObject);
        }
    }
}