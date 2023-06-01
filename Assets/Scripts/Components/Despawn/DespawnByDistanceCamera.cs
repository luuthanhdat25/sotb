using UnityEngine;

namespace Despawn
{
    public class DespawnByDistanceCamera : DespawnByDistance
    {
        [SerializeField] protected Transform mainCam;
        [SerializeField] protected float distanceLimit = 15f;

        protected override void LoadComponents() => this.LoadCamera();

        protected virtual void LoadCamera()
        {
            if (this.mainCam != null) return;
            this.mainCam = Transform.FindObjectOfType<Camera>().transform;
        }

        protected virtual void Start()
        {
            SetDistanceLimit(distanceLimit);
            SetTargetDistance(mainCam.position);
        }
    }
}