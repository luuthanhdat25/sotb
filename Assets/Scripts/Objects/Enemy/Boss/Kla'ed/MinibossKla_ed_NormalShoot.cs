using System.Collections;
using Comman;
using Objects.Enemy.AttackEnemy;
using UnityEngine;

namespace Enemy.Boss
{
    public class MinibossKla_ed_NormalShoot : Shoot
    {
        [SerializeField] private Transform leftGunPosition;
        [SerializeField] private Transform rightGunPosition;
        [SerializeField] private MinibossKla_ed_Animation minibossKlaEdAnimation;
        protected override void Fire()
        {
            if (isFiring && firingCoroutine == null)
            {
                firingCoroutine = StartCoroutine(FireContinously());
                if(minibossKlaEdAnimation != null) minibossKlaEdAnimation.SetIsFiring(true);
            }
            else
            {
                if(minibossKlaEdAnimation != null) minibossKlaEdAnimation.SetIsFiring(false);
            }
        }
        
        IEnumerator FireContinously()
        {
            Transform newProjectileLeft = EnemyProjectileSpawner.Instance.Spawn(EnemyProjectileSpawner.Instance.projectile1_Miniboss_Kla_ed1);
            this.SetActiveProjectileAndMoveToNewPosition(newProjectileLeft, leftGunPosition.position);
            Transform newProjectileRight = EnemyProjectileSpawner.Instance.Spawn(EnemyProjectileSpawner.Instance.projectile1_Miniboss_Kla_ed2);
            this.SetActiveProjectileAndMoveToNewPosition(newProjectileRight, rightGunPosition.position);
            
            yield return new WaitForSeconds(firingRate);
            firingCoroutine = null;
        }
        
        private void SetActiveProjectileAndMoveToNewPosition(Transform projectile, Vector3 position)
        {
            if (projectile == null) return;
            projectile.position = position;
            projectile.gameObject.SetActive(true);
        }

        public float GetFiringRate() => this.firingRate;
        public void SetIsFiring(bool isFiring) => this.isFiring = isFiring;
        public void DoubleFiringRate() => this.firingRate /= 2;
    }
}