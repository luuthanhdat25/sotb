using Enemy;
using Enemy.Boss.Nairan.Miniboss.Boss;
using Enemy.Boss.Nairan.Miniboss.Boss.Battlecruiser;
using Objects.Enemy.AttackEnemy;
using UnityEngine;

public class BossNairanBattlecruiserFollowAndShootShockWaveSMB : AbsFollowAndShootSMB
{
    protected override float GetRandomYValue() => Random.Range(-0.5f, 1.2f);
    
    protected override float GetSpeedFollow()
        => DoubleBossNairanCtrl.Instance.SpeedFollowSWBattlecruiser;

    protected override float GetTimeShootOneTime()
        => DoubleBossNairanCtrl.Instance.TimeShootOneTimeSWBattlecruiser;

    protected override int GetNumberOfActack()
        => DoubleBossNairanCtrl.Instance.NumOfShootAttackSWBattlecruiser;

    protected override void SetProjectile(bool isOn)
        => BossNairanBattlecruiserCtrl.Instance.BossNairanBattlecruiserShootShockWave.SetIsFiring(isOn);

    protected override void UnSetAnimation()
        => BossNairanBattlecruiserCtrl.Instance.SetIsFollowAndShootShockWave(false);
}
