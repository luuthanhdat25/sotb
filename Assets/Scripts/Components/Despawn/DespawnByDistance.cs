using UnityEngine;

namespace Despawn
{
    public class DespawnByDistance : Despawn
    {
        protected float abstractDistanceLimit;
        protected Vector3 abstractTargetDistance;
        [SerializeField] protected float currentDistance = 0f;
        
        protected override bool CanDespawn()
        {
            this.currentDistance = Vector3.Distance(transform.parent.position, abstractTargetDistance);
            if (this.currentDistance > abstractDistanceLimit) return true;
            return false;
        }

        protected virtual void SetDistanceLimit(float distance)
        {
            this.abstractDistanceLimit = distance;
        }

        protected virtual void SetTargetDistance(Vector3 target)
        {
            this.abstractTargetDistance = target;
        }
    }
}