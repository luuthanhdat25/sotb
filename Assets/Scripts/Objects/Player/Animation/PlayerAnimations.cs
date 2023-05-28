using System.Collections;
using DefaultNamespace;
using UnityEngine;

namespace Player
{
    public class PlayerAnimations: RepeatMonoBehaviour
    {
        [SerializeField] private float deadAnimatorTime = 2;
        [SerializeField] private int healthStateAnimaiton = 4;
        [SerializeField] private Animator animator;
        
        [SerializeField] private Transform normalWeapon;
        [SerializeField] private Transform untiWeapon;
        [SerializeField] private Transform defaultEngine;
        [SerializeField] private Transform upgradeEngine;
        [SerializeField] private Transform shield;
        
        private const string IS_DESTRUCTION = "isDestruction";
        private const string IS_COMBACK_HEALTHSTATE = "comebackHealthState";
        
        
        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadAnimator();
        }

        private void LoadAnimator()
        {
            if (this.animator != null) return;
            this.animator = GetComponent<Animator>();
        }
        
        public void DestructionAnimation()
        { 
            //this.animator.SetBool(IS_DESTRUCTION, false);
            this.animator.SetBool(IS_DESTRUCTION, true);
            //this.animator.SetBool(IS_DESTRUCTION, false);
        }
        public void RebornAnimaiton()
        { 
            this.animator.SetBool(IS_DESTRUCTION, false);
            StartCoroutine(ComebackHealthState());
        }

        IEnumerator ComebackHealthState()
        {
            yield return new WaitForSeconds(deadAnimatorTime);
            this.animator.SetBool(IS_COMBACK_HEALTHSTATE, true);
           // this.animator.SetBool(IS_COMBACK_HEALTHSTATE, false);
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
        
        public void TurnOnShield()
        {
            if (this.shield == null) return;
            this.shield.gameObject.SetActive(true);
        }
        
        public void TurnOffShield()
        {
            if (this.shield == null) return;
            this.shield.gameObject.SetActive(false);
        }

        public void DeductHealthStateAnimaiton() => this.healthStateAnimaiton--;
    }
}