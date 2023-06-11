using Enemy.Boss.Nairan.Miniboss.Boss;
using Enemy.Boss.Nairan.Miniboss.Boss.Battlecruiser;
using UnityEngine;

public class BossNairanBattlecruiserIdleSMB : AbsIdleSMB
{
    protected override void ChangeState(Animator animator)
    {
        //if (DoubleBossNairanCtrl.Instance.CanGetStateNumber == false) return;
        //DoubleBossNairanCtrl.Instance.SetCanGetStateNumber(false);
        if (DoubleBossNairanCtrl.Instance.StateNumber == 1)
            BossNairanBattlecruiserCtrl.Instance.SetIsLazerSlide(true);
        else
            BossNairanBattlecruiserCtrl.Instance.SetIsFollowAndShootShockWave(true);
    }

    protected override Vector3 GetDefaultPosition()
        => BossNairanBattlecruiserCtrl.Instance.GetDefaultPosition();
    
    protected override float GetTimeWait()
        => DoubleBossNairanCtrl.Instance.TimeWaitIdleOne;
}
