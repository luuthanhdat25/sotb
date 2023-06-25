using UnityEngine;

namespace Enemy.Boss.Nairan.Miniboss
{
    public class MinibossNairanModelShipAnimation : MonoBehaviour
    {
        private Animator _animator;
        
        private enum AnimationiParameters
        {
            isDestruction,
            isShootLazer,
            isFollowAndShoot
        }
    
        private void Start() => _animator ??= GetComponent<Animator>();
        
        public void SetIsShootLazer(bool isOn) =>this._animator.SetBool(AnimationiParameters.isShootLazer.ToString(), isOn);
        
        public void SetIsFollowAndShoot(bool isOn) =>this._animator.SetBool(AnimationiParameters.isFollowAndShoot.ToString(), isOn);
        
        public void SetIsDestructionTrigger() =>this._animator.SetTrigger(AnimationiParameters.isDestruction.ToString());
    }
}