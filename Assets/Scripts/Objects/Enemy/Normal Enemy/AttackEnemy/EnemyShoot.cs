using System;
using System.Collections;
using Comman;
using Objects.Enemy.AttackEnemy;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemy
{
    public class EnemyShoot : Shoot
    {
        private void Start()
        {
            isFiring = true;
        }
        
        protected override void Fire()
        {
            if (isFiring && firingCoroutine == null) firingCoroutine = StartCoroutine(FireContinously());
            else
            {
                if (!isFiring && firingCoroutine != null)
                {
                    StopCoroutine(firingCoroutine);
                    firingCoroutine = null;
                }
            }
        }

        IEnumerator FireContinously()
        {
            while (true)
            {
                Transform newProjectile = EnemyProjectileSpawner.Instance.Spawn(EnemyProjectileSpawner.Instance.projectile1);
                
                if (newProjectile != null)
                {
                    newProjectile.transform.position = transform.parent.position;
                    newProjectile.gameObject.SetActive(true);
                }
                
                yield return new WaitForSeconds(firingRate);
            }
        }
    }
}