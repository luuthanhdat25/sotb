using Enemy;
using UnityEngine;

public class BossNautolanFollowAndShootShockWaveSMB : AbsFollowAndShootSMB
{
    protected override float GetSpeedFollow() => BossNautolanCtrl.Instance.GetSpeedFollowShockWave();
    
    protected override float GetTimeShootOneTime() => BossNautolanCtrl.Instance.GetTimeShootOneTimeShockWave();
    
    protected override int GetNumberOfActack() => BossNautolanCtrl.Instance.GetNumberOfAttacksShockWave();

    protected override void SetProjectile(bool isOn)
    {
        BossNautolanCtrl.Instance.BossNautolanShockWaveShoot.SetIsFiring(isOn);
        BossNautolanCtrl.Instance.BossNautolanModelShipAnimation.SetIsShootShockWave(isOn);
    }
    
    protected override float GetRandomYValue() => Random.Range(-1.7f, 1.2f);
    
    protected override void UnSetAnimation() => BossNautolanCtrl.Instance.SetIsShockWave(false);
}
