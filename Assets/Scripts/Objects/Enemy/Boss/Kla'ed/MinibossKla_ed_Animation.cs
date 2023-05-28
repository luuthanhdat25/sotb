using UnityEngine;

namespace Enemy.Boss
{
    public class MinibossKla_ed_Animation : MonoBehaviour
    {
        private const string IS_FIRING = "isFiring";
        private const string IS_UNTI = "isUnti";
        private const string IS_DESTRUCTION = "isDestruction";
        private Animator _animator;

        private void Start()
        {
            if (_animator != null) return;
            _animator = GetComponent<Animator>();
        }
        
        public void SetIsFiring(bool isFiring) => this._animator.SetBool(IS_FIRING, isFiring);
        public void SetIsUntiTrigger() => this._animator.SetTrigger(IS_UNTI);
        
        public void SetIsDestructionTrigger() =>this._animator.SetTrigger(IS_DESTRUCTION);
    }
}