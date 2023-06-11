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
        => BossNairanBattlecruiserCtrl.Instance.BossNairanBattlecruiserShootShockWave.SetIsFiring(isOn);

    protected override void UnSetAnimation()
    {
        //DoubleBossNairanCtrl.Instance.SwapDefaultPosition();
        BossNairanBattlecruiserCtrl.Instance.SetIsFinishBehaviour(true);
        /*if (BossNairanDreadnoughtCtrl.Instance.IsFinishBehaviour)
        {
            BossNairanBattlecruiserCtrl.Instance.SetIsFinishBehaviour(false);*/
            BossNairanBattlecruiserCtrl.Instance.SetIsFollowAndShootShockWave(false);
        
    }
}
