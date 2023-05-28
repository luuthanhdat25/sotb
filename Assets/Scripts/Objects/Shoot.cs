using UnityEngine;

namespace Comman
{
    public abstract class Shoot : MonoBehaviour
    {
        [Header("Shoot")]
        [SerializeField] protected float firingRate;
        [SerializeField] protected bool isFiring;

        protected Coroutine firingCoroutine;
        
        protected virtual void FixedUpdate()
        {
            Fire();
        }

        protected abstract void Fire();
    }
}