using System.Collections;
using Comman;
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
            if (!isHasUntiWeapon) return;
            if (!GameInput.Instance.IsUntiPressed()) return;
            
            this.ResetIsFiring();
            if (firingCoroutine == null && this.isFiring)
                firingCoroutine = StartCoroutine(FireContinously());
        }
        
        private void ResetIsFiring()
        {
            //Check all condition for Firing
            this.isFiring = IsEnoughEnergiesForUnti();
        }
        
        private bool IsEnoughEnergiesForUnti() => PlayerCtrl.Instance.PlayerEnergies.IsEnoughEnergies(this.boostCeilsUse);
        
        private IEnumerator FireContinously()
        {
            isFire = true;
            yield return new WaitForSeconds(delayAnimaitonTime);
            
            Transform newProjectile = PlayerProjectileSpawner.Instance.Spawn(PlayerProjectileSpawner.Instance.unti);
            if (newProjectile != null)
            {
                PlayerCtrl.Instance.PlayerBootCeils.DeductCeilsByValue(this.boostCeilsUse);
                
                newProjectile.transform.position = transform.parent.position;
                newProjectile.gameObject.SetActive(true);
            }
            
            isFire = false;
            yield return new WaitForSeconds(firingRate);
            firingCoroutine = null;
            
        }

        public bool GetIsFire() =>  this.isFire;
        public void SetIsHasUntiWeaponTrue() => this.isHasUntiWeapon = true;
    }
}