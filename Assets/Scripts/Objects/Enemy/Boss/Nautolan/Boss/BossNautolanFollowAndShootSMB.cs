using Enemy;
using UnityEngine;

public class BossNautolanFollowAndShootSMB : AbsFollowAndShootSMB
{
    protected override float GetSpeedFollow() => BossNautolanCtrl.Instance.GetSpeedFollow();
    
    protected override float GetTimeShootOneTime() => BossNautolanCtrl.Instance.GetTimeShootOneTime();
    
    protected override int GetNumberOfActack() => BossNautolanCtrl.Instance.GetNumberOfAttacksFollowAndShoot();
    
    protected override void SetProjectile(bool isOn) => BossNautolanCtrl.Instance.BossNautolanSpinningBulletShoot.SetIsFiring(isOn);
    
    protected override void UnSetAnimation() => BossNautolanCtrl.Instance.SetIsFollowAndShoot(false);
    
    protected override float GetRandomYValue() => Random.Range(-1.7f, 1f);
}
