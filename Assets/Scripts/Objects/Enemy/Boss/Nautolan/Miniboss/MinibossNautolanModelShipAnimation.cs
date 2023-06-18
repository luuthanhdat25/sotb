using UnityEngine;

public class MinibossNautolanModelShipAnimation : RepeatMonoBehaviour
{
    [SerializeField] private Animator _animator;
 
    private enum AnimationParameter
    {
        isDestruction,
        isFollowAndShoot,
        isChasePlayer
    }
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (_animator != null) return;
        _animator = GetComponent<Animator>();
    }

    public void SetIsDestructionTrigger() =>this._animator.SetTrigger(AnimationParameter.isDestruction.ToString());
    public void SetIsFollowAndShoot(bool isOn) =>this._animator.SetBool(AnimationParameter.isFollowAndShoot.ToString(), isOn);
    public void SetIsChasePlayer(bool isOn) =>this._animator.SetBool(AnimationParameter.isChasePlayer.ToString(), isOn);
}
