using UnityEngine;

namespace Objects.Enemy.Boss.Nairan.Dreadnought
{
    public class BossNairanDreadnoughtModelShipAnimation : MonoBehaviour
    {
        private Animator _animator;
    
        private enum AnimatorParameter
        {
            isDestruction,
            isLazerSlide,
            isFollowAndShootNormal,
            isSpawnTorpedo,
            isArcShootNormal,
            isTeleportAndShootLazer
        }
        
        private void Start() => _animator ??= GetComponent<Animator>();

        public void SetIsDestructionTrigger() =>this._animator.SetTrigger(AnimatorParameter.isDestruction.ToString());
        
        public void SetIsLazerSlide(bool isOn) =>this._animator.SetBool(AnimatorParameter.isLazerSlide.ToString(), isOn);
        
        public void SetIsFollowAndShootNormal(bool isOn) =>this._animator.SetBool(AnimatorParameter.isFollowAndShootNormal.ToString(), isOn);
        
        public void SetIsSpawnTorpedo(bool isOn) =>this._animator.SetBool(AnimatorParameter.isSpawnTorpedo.ToString(), isOn);
        
        public void SetIsArcShootNormal(bool isOn) =>this._animator.SetBool(AnimatorParameter.isArcShootNormal.ToString(), isOn);

        public void SetIsTeleportAndShootLazer(bool isOn) =>this._animator.SetBool(AnimatorParameter.isTeleportAndShootLazer.ToString(), isOn);
    }
}