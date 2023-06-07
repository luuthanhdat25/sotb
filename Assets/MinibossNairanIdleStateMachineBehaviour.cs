using System.Collections;
using System.Collections.Generic;
using Enemy.Boss.Nairan.Miniboss;
using UnityEngine;

public class MinibossNairanIdleStateMachineBehaviour : IdleStateMachineBehaviour
{
    protected override void ChangeState(Animator animator)
    {
        int stateNumber = GetRandomState(2);
        Debug.Log("Miniboss Nairan boss state: " + stateNumber);
        if(stateNumber == 1) 
            MiniBossNairanCtrl.Instance.SetIsFollowAndShoot(true);
        else
            MiniBossNairanCtrl.Instance.SetIsLazerSlide(true);
    }

    protected override Vector3 GetDefaultPosition() => MinibossNautolanCtrl.Instance.GetDefaultPosition();
    protected override float GetTimeWait() => MinibossNautolanCtrl.Instance.GetTimeWaitIdle();
}
