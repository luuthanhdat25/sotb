using UnityEngine;

namespace Enemy.Boss
{
    public class MinibossKla_edModelShipAnimation : MonoBehaviour
    {
        private Animator animator;
        private enum AnimationParameter
        {
            isFollowAndShoot,
            isChasePlayer,
            isDestruction
        }

        private void Start() => animator ??= GetComponent<Animator>();

        public void SetIsFiring(bool isFiring) => this.animator.SetBool(AnimationParameter.isFollowAndShoot.ToString(), isFiring);
        
        public void SetIsChasePlayer(bool isFiring) => this.animator.SetBool(AnimationParameter.isChasePlayer.ToString(), isFiring);
        
        public void SetIsDestructionTrigger() =>this.animator.SetTrigger(AnimationParameter.isDestruction.ToString());
    }
}