using System.Collections;
using Damage;
using UnityEngine;

namespace Player
{
    public class PlayerDamageReciever : DamageReceiver
    {
        [SerializeField] private int energiesDeductWhenPlayerDead = 1;
        [SerializeField] private float timeDelayReborn = 2f;
        [SerializeField] private CircleCollider2D circleCollider2D;
        [SerializeField] private bool isShieldUp = false;
        
        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadColliderComponent();
        }

        private void LoadColliderComponent()
        {
            if (this.circleCollider2D != null) return;
            this.circleCollider2D = GetComponent<CircleCollider2D>();
        }
        
        protected override void OnDead()
        {
            this.Dead();
            if (PlayerCtrl.Instance.PlayerEnergies.GetCurrentEnergies() > 0)
                StartCoroutine(this.RebornCoroutine());
        }
        
        private void Dead()
        {
            PlayerCtrl.Instance.PlayerEnergies.DeductEnergies(energiesDeductWhenPlayerDead);
            PlayerCtrl.Instance.PlayerAnimations.DestructionAnimation();
            PlayerCtrl.Instance.PlayerMovement.SetCanMoveNormal(false);
            PlayerCtrl.Instance.SetPlayerDead(true);
        }

        private IEnumerator RebornCoroutine()
        {
            this.SetActiveCollider(false);
            yield return new WaitForSeconds(this.timeDelayReborn);
            this.CombackToDefaultPositionWithRebornAnimation();
            yield return new WaitForSeconds(PlayerCtrl.Instance.PlayerAnimations.GetDeadAnimatorTime());
            hpCurrent = hpMax;
            this.SetActiveCollider(true);
        }

        private void CombackToDefaultPositionWithRebornAnimation()
        {
            PlayerCtrl.Instance.PlayerAnimations.RebornAnimaiton();
            transform.parent.position = PlayerCtrl.Instance.GetDefaultPosition();
            PlayerCtrl.Instance.SetPlayerDead(false);
            PlayerCtrl.Instance.PlayerMovement.SetCanMoveNormal(true);
        }

        public override void Deduct(int hpDeduct)
        {
            if (isShieldUp)
            {
                PlayerCtrl.Instance.PlayerAnimations.SetActiveShield(false);
                isShieldUp = false;
            }
            base.Deduct(hpDeduct);
        }

        public void SetActiveCollider(bool isOn) => this.circleCollider2D.enabled = isOn;

        public void ShieldUp()
        {
            if(hpCurrent == 1) this.hpCurrent++;
            isShieldUp = true;
        }
    }
}