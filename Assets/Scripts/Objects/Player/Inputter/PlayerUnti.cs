using System.Collections;
using Comman;
using Damage.RhythmScripts;
using DefaultNamespace;
using UnityEngine;

namespace Player
{
    public class PlayerUnti : Shoot
    {
        [SerializeField] private int boostCeilsUse = 2;
        [SerializeField] private bool isHasUntiWeapon = false;
        private float delayAnimaitonTime = 0.5f;
        private bool isFire = false;
        
        protected override void Fire()
        {
            if (PlayerCtrl.Instance.GetIsPlayerDead() || !isHasUntiWeapon) return;
            if (!GameInput.Instance.IsUntiPressed()) return;
            
            isFiring = IsBoostCeilForUnti();
            if (firingCoroutine == null && this.isFiring)
                firingCoroutine = StartCoroutine(FireContinously());
        }

        private bool IsBoostCeilForUnti() => PlayerCtrl.Instance.PlayerBootCeils.IsEnough(this.boostCeilsUse);
        
        private IEnumerator FireContinously()
        {
            isFire = true;
            PlayerCtrl.Instance.PlayerMovement.SetCanMoveNormal(false);
            yield return new WaitForSeconds(delayAnimaitonTime);
            //Shoot
            Transform newProjectile = PlayerProjectileSpawner.Instance.Spawn(PlayerProjectileSpawner.Instance.unti);
            if (newProjectile != null)
            {
                PlayerCtrl.Instance.PlayerBootCeils.DeductCeilsByValue(this.boostCeilsUse);
                AudioSpawner.Instance.SpawnPlayerEffect(AudioSpawner.SoundEffectEnum.UntiPlayer);
                newProjectile.transform.position = transform.parent.position;
                newProjectile.gameObject.SetActive(true);
            }
            
            PlayerCtrl.Instance.PlayerParticleEffect.UntiShootEffect();
            
            PlayerCtrl.Instance.PlayerMovement.SetCanMoveNormal(true);
            isFire = false;
            yield return new WaitForSeconds(firingRate);
            firingCoroutine = null;
            
        }

        public bool GetIsFire() =>  this.isFire;
        public void SetIsHasUntiWeaponTrue() => this.isHasUntiWeapon = true;
    }
}