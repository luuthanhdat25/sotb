using System.Collections;
using Comman;
using UnityEngine;

namespace Enemy
{
    public abstract class AbsEnemyShoot : Shoot
    {
        protected override void Fire()
        {
            if (isFiring && firingCoroutine == null)
                firingCoroutine = StartCoroutine(FireContinously());
        }
        
        protected virtual IEnumerator FireContinously()
        {
            Transform newProjectile = GetProjectile();
            if (newProjectile != null)
            {
                newProjectile.position = transform.parent.position;
                newProjectile.gameObject.SetActive(true);
            }
        
            yield return new WaitForSeconds(firingRate);
            firingCoroutine = null;
        }

        protected abstract Transform GetProjectile();
    }
}