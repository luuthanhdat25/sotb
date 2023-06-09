using Enemy.Boss.Nairan.Miniboss;
using UnityEngine;

public class MinibossNairanIdleStateMachineBehaviour : IdleStateMachineBehaviour
{
    protected override void ChangeState(Animator animator)
    {
        int stateNumber = GetRandomState(2);
        Debug.Log("Miniboss Nairan boss state: " + stateNumber);
        if(stateNumber == 1) 
            MiniBossNairanCtrl.Instance.SetIsLazerSlide(true);
        else
            MiniBossNairanCtrl.Instance.SetIsFollowAndShoot(true);
    }

    protected override Vector3 GetDefaultPosition() => MiniBossNairanCtrl.Instance.GetDefaultPosition();
    protected override float GetTimeWait() => MiniBossNairanCtrl.Instance.GetTimeWaitIdle();
}
