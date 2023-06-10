using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class BossNautolanFollowAndShootShockWaveBehaviour : AbsFollowAndShootSMB
{
    protected override float GetSpeedFollow() => BossNautolanCtrl.Instance.GetSpeedFollowShockWave();
    protected override float GetTimeShootOneTime() => BossNautolanCtrl.Instance.GetTimeShootOneTimeShockWave();
    protected override int GetNumberOfActack() => BossNautolanCtrl.Instance.GetNumberOfAttacksShockWave();
    protected override void SetProjectile(bool isOn) => BossNautolanCtrl.Instance.BossNautolanShockWaveShoot.SetIsFiring(isOn);
    protected override void UnSetAnimation() => BossNautolanCtrl.Instance.SetIsShockWave(false);
}
