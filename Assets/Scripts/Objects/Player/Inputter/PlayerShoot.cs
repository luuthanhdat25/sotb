using System.Collections;
using Comman;
using DefaultNamespace;
using Player.Animation;
using UnityEngine;

namespace Player
{
    public class PlayerShoot : Shoot
    {
        [SerializeField] private Transform leftGunPosition;
        [SerializeField] private Transform rightGunPosition;
        [SerializeField] private bool isHasWeapon = false;
        [SerializeField] private PlayerAnimationBaseShooting playerAnimationBaseShooting; 
        
        protected override void Fire()
        {
            if (PlayerCtrl.Instance.GetIsPlayerDead() || !isHasWeapon) return;
            if (!GameInput.Instance.IsFirePressed()) return;
            isFiring = GameInput.Instance.IsFirePressed();

            if (isFiring && firingCoroutine == null)
            {
                firingCoroutine = StartCoroutine(FireContinously());
                if(playerAnimationBaseShooting != null) playerAnimationBaseShooting.SetIsFiring(true);
            }
            else
            {
                if(playerAnimationBaseShooting != null) playerAnimationBaseShooting.SetIsFiring(false);
            }
        }

        IEnumerator FireContinously()
        {
            Transform newProjectileLeft = PlayerProjectileSpawner.Instance.Spawn(PlayerProjectileSpawner.Instance.projectile1);
            this.SetActiveProjectileAndMoveToNewPosition(newProjectileLeft, leftGunPosition.position);
            Transform newProjectileRight = PlayerProjectileSpawner.Instance.Spawn(PlayerProjectileSpawner.Instance.projectile2);
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

        public void SetIsHasWeapon(bool isOn) => this.isHasWeapon = isOn;

        public void DecreaseFireRateInTime(float value, float time)
        {
            StartCoroutine(CouroutineDecreaseFireRate(value, time));
        }

        IEnumerator CouroutineDecreaseFireRate(float value, float time)
        {
            if (this.firingRate <= 0.1f)
            {
                this.firingRate = 0.1f;
                yield break;
            }
            this.firingRate /= value;
            yield return new WaitForSeconds(time);
            this.firingRate *= value - (value / 10);
            if (this.firingRate <= 0.1f) this.firingRate = 0.1f;
        }
    }
}