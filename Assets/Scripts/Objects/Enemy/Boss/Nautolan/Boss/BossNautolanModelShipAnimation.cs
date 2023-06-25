using UnityEngine;

public class BossNautolanModelShipAnimation : RepeatMonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    private enum AnimationParameter
    {
        isDestruction,
        isFollowAndShoot,
        isArcShoot,
        isBomDrop,
        isShootShockWave
    }
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        _animator ??= GetComponent<Animator>();
    }

    public void SetIsDestructionTrigger() =>this._animator.SetTrigger(AnimationParameter.isDestruction.ToString());
    
    public void SetIsFollowAndShoot(bool isOn) =>this._animator.SetBool(AnimationParameter.isFollowAndShoot.ToString(), isOn);

    public void SetIsArcShoot(bool isOn) =>this._animator.SetBool(AnimationParameter.isArcShoot.ToString(), isOn);
    
    public void SetIsBomDrop(bool isOn) =>this._animator.SetBool(AnimationParameter.isBomDrop.ToString(), isOn);
    
    public void SetIsShootShockWave(bool isOn) =>this._animator.SetBool(AnimationParameter.isShootShockWave.ToString(), isOn);
}
