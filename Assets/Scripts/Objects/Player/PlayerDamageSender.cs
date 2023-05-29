using System.Collections;
using Damage;
using UnityEngine;

namespace Player
{
    public class PlayerDamageSender : DamageSender
    {
        [SerializeField] private int energiesDeductWhenPlayerDead = 2;
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
        
        public override void GotHit()
        {
            base.GotHit();
            if (isShieldUp) return;
            this.Dead();
            if (PlayerCtrl.Instance.PlayerEnergies.GetCurrentEnergies() > 0)
                StartCoroutine(this.Reborn());
        }

        private void Dead()
        {
            PlayerCtrl.Instance.PlayerEnergies.DeductEnergies(energiesDeductWhenPlayerDead);
            PlayerCtrl.Instance.PlayerAnimations.DestructionAnimation();
            PlayerCtrl.Instance.PlayerMovement.SetCanMoveNormal(false);
            PlayerCtrl.Instance.SetPlayerDead(true);
            CameraAnimation.Instance.ShakeCamera();
        }

        private IEnumerator Reborn()
        {
            this.TurnOffCollider();
            yield return new WaitForSeconds(this.timeDelayReborn);
            this.CombackToDefaultPositionWithRebornAnimation();
            yield return new WaitForSeconds(PlayerCtrl.Instance.PlayerAnimations.GetDeadAnimatorTime());
            this.TurnOnCollider();
        }

        private void CombackToDefaultPositionWithRebornAnimation()
        {
            PlayerCtrl.Instance.PlayerAnimations.RebornAnimaiton();
            transform.parent.position = PlayerCtrl.Instance.GetDefaultPosition();
            PlayerCtrl.Instance.SetPlayerDead(false);
            PlayerCtrl.Instance.PlayerMovement.SetCanMoveNormal(true);
        }

        public void TurnOnCollider() => this.circleCollider2D.enabled = true;
        public void TurnOffCollider()
        {
            this.circleCollider2D.enabled = false;
            Debug.Log("turn off collider");
        }

        public void SetIsShieldUp(bool isUp) => this.isShieldUp = isUp;
    }
}