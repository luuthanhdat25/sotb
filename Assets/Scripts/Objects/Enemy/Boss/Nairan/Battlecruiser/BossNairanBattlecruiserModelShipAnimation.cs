using UnityEngine;

namespace Enemy.Boss.Nairan.Miniboss.Boss.Battlecruiser
{
    public class BossNairanBattlecruiserModelShipAnimation : MonoBehaviour
    {
        private Animator _animator;
    
        private enum AnimatorParameter
        {
            isDestruction,
            isLazerSlide,
            isFollowAndShootShockWave,
            isFollowAndShootLazer
        }
        
        private void Start() => _animator ??= GetComponent<Animator>();

        public void SetIsDestructionTrigger() =>this._animator.SetTrigger(AnimatorParameter.isDestruction.ToString());
        
        public void SetIsLazerSlide(bool isOn) =>this._animator.SetBool(AnimatorParameter.isLazerSlide.ToString(), isOn);

        public void SetIsFollowAndShootShockWave(bool isOn) =>this._animator.SetBool(AnimatorParameter.isFollowAndShootShockWave.ToString(), isOn);
        
        public void SetIsFollowAndShootLazer(bool isOn) =>this._animator.SetBool(AnimatorParameter.isFollowAndShootLazer.ToString(), isOn);
    }
}