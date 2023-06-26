using Enemy;
using Enemy.Boss.Nairan.Miniboss.Boss;
using Enemy.Boss.Nairan.Miniboss.Boss.Battlecruiser;
using Objects.Enemy.Boss.Nairan.Dreadnought;
using Random = UnityEngine.Random;

public class BossNairanBattlecruiserFollowAndShootShockWaveSMB : AbsFollowAndShootSMB
{
    protected override float GetRandomYValue() => Random.Range(0.5f, 1.7f);
    
    protected override float GetSpeedFollow()
        => DoubleBossNairanCtrl.Instance.SpeedFollowSWBattlecruiser;

    protected override float GetTimeShootOneTime()
        => DoubleBossNairanCtrl.Instance.TimeShootOneTimeSWBattlecruiser;

    protected override int GetNumberOfActack()
        => DoubleBossNairanCtrl.Instance.NumOfShootAttackSWBattlecruiser;

    protected override void SetProjectile(bool isOn)
    {
        BossNairanBattlecruiserCtrl.Instance.BossNairanBattlecruiserModelShipAnimation.SetIsFollowAndShootShockWave(isOn);
        BossNairanBattlecruiserCtrl.Instance.BossNairanBattlecruiserShootShockWave.SetIsFiring(isOn);
    }

    protected override void UnSetAnimation()
    {
        BossNairanBattlecruiserCtrl.Instance.SetIsFinishBehaviour(true);
        if (BossNairanDreadnoughtCtrl.Instance.IsFinishBehaviour)
        {
            DoubleBossNairanCtrl.Instance.SwapDefaultPosition();
            //BossNairanDreadnoughtCtrl.Instance.SetIsFinishBehaviour(false);
            BossNairanBattlecruiserCtrl.Instance.SetIsFollowAndShootShockWave(false);
        }
    }
}
