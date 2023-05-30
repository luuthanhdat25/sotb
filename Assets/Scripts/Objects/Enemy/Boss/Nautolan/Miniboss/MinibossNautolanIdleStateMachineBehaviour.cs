using UnityEngine;

public class MinibossNautolanIdleStateMachineBehaviour : IdleStateMachineBehaviour
{
    protected override void ChangeState(Animator animator)
    {
        int stateNumber = GetRandomState(2);
        Debug.Log("Miniboss Nautolan boss state: " + stateNumber);
        if(stateNumber == 1) 
            MinibossNautolanCtrl.Instance.SetIsFollowAndShoot(true);
        else
            MinibossNautolanCtrl.Instance.SetIsChasePlayer(true);
    }

    protected override Vector3 GetDefaultPosition() => MinibossNautolanCtrl.Instance.GetDefaultPosition();
    protected override float GetTimeWait() => MinibossNautolanCtrl.Instance.GetTimeWaitIdle();
}
