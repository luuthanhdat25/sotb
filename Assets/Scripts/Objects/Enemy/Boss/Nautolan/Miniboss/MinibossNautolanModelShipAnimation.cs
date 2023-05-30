using UnityEngine;

public class MinibossNautolanModelShipAnimation : RepeatMonoBehaviour
{
    private const string IS_DESTRUCTION = "isDestruction";
    [SerializeField] private Animator _animator;
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (_animator != null) return;
        _animator = GetComponent<Animator>();
    }

    public void SetIsDestructionTrigger() =>this._animator.SetTrigger(IS_DESTRUCTION);
}
