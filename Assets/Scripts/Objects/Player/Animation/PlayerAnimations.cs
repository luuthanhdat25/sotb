using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine;

namespace Player
{
    public class PlayerAnimations: RepeatMonoBehaviour
    {
        [SerializeField] private float deadAnimatorTime = 2;
        [SerializeField] private int healthStateAnimaiton = 4;
        [SerializeField] private Animator modelAnimator;
        [SerializeField] private Animator shieldAnimator;
        
        [SerializeField] private Transform normalWeapon;
        [SerializeField] private Transform untiWeapon;
        [SerializeField] private Transform defaultEngine;
        [SerializeField] private Transform upgradeEngine;
        [SerializeField] private Transform shield;
        
        private enum AnimatorParameter
        {
            isDestruction,
            comebackHealthState
        }
        
        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadModelAnimator();
        }

        private void LoadModelAnimator()
            => this.modelAnimator ??= GetComponent<Animator>();
        
        private void LoadShieldAnimator() 
            => this.shieldAnimator ??= GetComponentInChildren<Animator>();

        public void DestructionAnimation() 
            => this.modelAnimator.SetBool(AnimatorParameter.isDestruction.ToString(), true);

        public void RebornAnimaiton()
        { 
            this.modelAnimator.SetBool(AnimatorParameter.isDestruction.ToString(), false);
            StartCoroutine(ComebackHealthState());
        }

        IEnumerator ComebackHealthState()
        {
            yield return new WaitForSeconds(deadAnimatorTime);
            this.modelAnimator.SetBool(AnimatorParameter.comebackHealthState.ToString(), true);
        }

        public float GetDeadAnimatorTime() => this.deadAnimatorTime;
        
        public void SetBurstEngineUpgrade()
        {
            if (defaultEngine == null && upgradeEngine == null) return;
            defaultEngine.gameObject.SetActive(false);
            upgradeEngine.gameObject.SetActive(true);
        }

        public void TurnOnDefaultEngine()
        {
            if (this.defaultEngine == null) return;
            this.defaultEngine.gameObject.SetActive(true);
        }

        public void TurnOnNormalWeapon()
        {
            if (this.normalWeapon == null) return;
            this.normalWeapon.gameObject.SetActive(true);
        }
        
        public void TurnOnUntiWeapon()
        {
            if (this.untiWeapon == null) return;
            this.untiWeapon.gameObject.SetActive(true);
        }
        
        public void SetActiveShield(bool isOn)
        {
            if (this.shield == null) return;
            this.shield.gameObject.SetActive(isOn);
        }

        public void ShieldDestructionAfterTime(float time)
        {
            if (this.shieldAnimator == null) return;
            StartCoroutine(DestructionShield(time - 2));
        }

        private IEnumerator DestructionShield(float time)
        {
            yield return new WaitForSeconds(time);
            shieldAnimator.SetTrigger(AnimatorParameter.isDestruction.ToString());
        }

        public void DeductHealthStateAnimaiton() => this.healthStateAnimaiton--;
    }
}