using UnityEngine;

namespace Enemy.Boss
{
    public class MinibossKla_ed_Animation : MonoBehaviour
    {
        private enum AnimationParameter
        {
            isFiring,
            isChasePlayer,
            isDestruction
        }
        private Animator _animator;

        private void Start()
        {
            if (_animator != null) return;
            _animator = GetComponent<Animator>();
        }
        
        public void SetIsFiring(bool isFiring) => this._animator.SetBool(AnimationParameter.isFiring.ToString(), isFiring);
        public void SetIsChasePlayer(bool isFiring) => this._animator.SetBool(AnimationParameter.isChasePlayer.ToString(), isFiring);
        public void SetIsDestructionTrigger() =>this._animator.SetTrigger(AnimationParameter.isDestruction.ToString());
    }
}