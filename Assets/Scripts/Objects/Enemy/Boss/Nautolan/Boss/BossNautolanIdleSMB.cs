using UnityEngine;

public class BossNautolanIdleSMB : AbsIdleSMB
{
    protected override void ChangeState(Animator animator)
    {
        int stateNumber;
        if (!BossNautolanCtrl.Instance.BossNautolanDamageReciever.IsLowerHealth(60))
            stateNumber = GetRandomState(1, 3);
        else 
            stateNumber = GetRandomState(2, 4);
        Debug.Log("Boss Nautolan boss state: " + stateNumber);
        switch (stateNumber)
        {
            case 1: BossNautolanCtrl.Instance.SetIsFollowAndShoot(true);break;
            case 2: BossNautolanCtrl.Instance.SetIsArcShoot(true);break;
            case 3: BossNautolanCtrl.Instance.SetIsBomDrop(true);break;
            case 4: BossNautolanCtrl.Instance.SetIsShockWave(true);break;
        }
    }

    protected int GetRandomState(int min, int max) => Random.Range(min, max + 1);
    protected override Vector3 GetDefaultPosition() => BossNautolanCtrl.Instance.GetDefaultPosition();
    protected override float GetTimeWait() => BossNautolanCtrl.Instance.GetTimeWaitIdle();
}
