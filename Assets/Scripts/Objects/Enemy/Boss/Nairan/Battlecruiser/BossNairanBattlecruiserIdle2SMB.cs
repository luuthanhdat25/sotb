using Enemy.Boss.Nairan.Miniboss.Boss;
using Enemy.Boss.Nairan.Miniboss.Boss.Battlecruiser;
using UnityEngine;

public class BossNairanBattlecruiserIdle2SMB : AbsIdleSMB
{
    protected override void ChangeState(Animator animator)
    {
        switch (DoubleBossNairanCtrl.Instance.StateNumber)
        {
            case 1: BossNairanBattlecruiserCtrl.Instance.SetIsRotateLazer(true); break;
            case 2: BossNairanBattlecruiserCtrl.Instance.SetIsArcShockWave(true); break;
            case 3: BossNairanBattlecruiserCtrl.Instance.SetIsFollowAndShootLazer(true); break;
        }
    }

    protected override Vector3 GetDefaultPosition()
        => BossNairanBattlecruiserCtrl.Instance.GetDefaultPosition();
    
    protected override float GetTimeWait()
        => DoubleBossNairanCtrl.Instance.TimeWaitIdleTwo;
}
