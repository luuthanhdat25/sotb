using Enemy;
using Player;
using UnityEngine;

public class MiniBossNautolanFollowAndShootSMB : AbsFollowAndShootSMB
{
    protected override float GetRandomYValue() => Random.Range(-1.5f, 1f);
    protected override float GetSpeedFollow() => MinibossNautolanCtrl.Instance.GetSpeedFollow();
    protected override float GetTimeShootOneTime() => MinibossNautolanCtrl.Instance.GetTimeShootOneTime();

    protected override int GetNumberOfActack() => MinibossNautolanCtrl.Instance.GetNumberOfAttacksFollowAndShootBehaviour();

    protected override void UnSetAnimation() => MinibossNautolanCtrl.Instance.SetIsFollowAndShoot(false); 
    protected override void SetProjectile(bool isOn)
    {
        MinibossNautolanCtrl.Instance.MinibossNautolanShoot.SetIsFiring(isOn);
        MinibossNautolanCtrl.Instance.MinibossNautolanModelShipAnimation.SetIsFollowAndShoot(isOn);
    }
}
