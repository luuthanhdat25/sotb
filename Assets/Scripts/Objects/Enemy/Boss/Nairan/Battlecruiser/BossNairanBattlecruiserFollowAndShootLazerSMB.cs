using Enemy;
using Enemy.Boss.Nairan.Miniboss.Boss.Battlecruiser;
using UnityEngine;

public class BossNairanBattlecruiserFollowAndShootLazerSMB : AbsFollowAndShootSMB
{
    protected override float GetSpeedFollow()
        => BossNairanBattlecruiserCtrl.Instance.SpeedFollow;

    protected override float GetTimeShootOneTime()
        => BossNairanBattlecruiserCtrl.Instance.TimeShootInOneTime;

    protected override int GetNumberOfActack()
        => BossNairanBattlecruiserCtrl.Instance.NumberOfShootAttacks;

    protected override void SetProjectile(bool isOn)
    {
        if(isOn) 
            BossNairanBattlecruiserCtrl.Instance.BossShootLazer.SpawnLazerAfterTime(BossNairanBattlecruiserCtrl.Instance.TimeDelayBeforeShoot);
        else 
            BossNairanBattlecruiserCtrl.Instance.BossShootLazer.DespawnLazer();
    }

    protected override void UnSetAnimation()
        => BossNairanBattlecruiserCtrl.Instance.SetIsFollowAndShootLazer(false);
}
