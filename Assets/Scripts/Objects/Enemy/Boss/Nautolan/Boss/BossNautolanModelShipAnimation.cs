using UnityEngine;

public class BossNautolanModelShipAnimation : RepeatMonoBehaviour
{
    private enum AnimationParameter
    {
        isDestruction,
        isFollowAndShoot,
        isArcShoot,
        isBomDrop
    }
    [SerializeField] private Animator _animator;
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (_animator != null) return;
        _animator = GetComponent<Animator>();
    }

    public void SetIsDestructionTrigger() =>this._animator.SetTrigger(AnimationParameter.isDestruction.ToString());
    public void SetIsFollowAndShoot(bool isOn) =>this._animator.SetBool(AnimationParameter.isFollowAndShoot.ToString(), isOn);
    public void SetIsArcShoot(bool isOn) =>this._animator.SetBool(AnimationParameter.isArcShoot.ToString(), isOn);
    public void SetIsBomDrop(bool isOn) =>this._animator.SetBool(AnimationParameter.isBomDrop.ToString(), isOn);
}
