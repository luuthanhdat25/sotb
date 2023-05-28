using UnityEngine;

namespace Projectile
{
    public class ProjectileModelRotateAroundCenter : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 300f;

        private void FixedUpdate()
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.fixedDeltaTime);
        }
    }
}