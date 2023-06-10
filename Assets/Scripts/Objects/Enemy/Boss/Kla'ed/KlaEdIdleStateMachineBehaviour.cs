using UnityEngine;

public class KlaEdIdleStateMachineBehaviour : AbsIdleSMB
{
    [SerializeField] private int numberOfState = 2;
    protected override void ChangeState(Animator animator)
    {
        int stateNumber = GetRandomState(numberOfState);
        Debug.Log("Kla_ed boss state: " + stateNumber);
        if(stateNumber == 1) 
            MiniBossKla_edCtrl.Instance.SetIsFollowAndShoot(true);
        else
            MiniBossKla_edCtrl.Instance.SetIsChasePlayer(true);
    }
    
    protected override Vector3 GetDefaultPosition() => MiniBossKla_edCtrl.Instance.GetDefaultPosition();
    protected override float GetTimeWait() => MiniBossKla_edCtrl.Instance.GetTimeWaitIdle();
}
