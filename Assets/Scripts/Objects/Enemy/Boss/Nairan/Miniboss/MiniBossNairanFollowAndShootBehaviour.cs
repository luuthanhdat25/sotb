using System.Collections;
using System.Collections.Generic;
using Enemy;
using Enemy.Boss.Nairan.Miniboss;
using UnityEngine;

public class MiniBossNairanFollowAndShootBehaviour : AbsFollowAndShootSMB
{
    protected override float GetSpeedFollow() => MiniBossNairanCtrl.Instance.GetSpeedFollow();
    protected override float GetTimeShootOneTime() => MiniBossNairanCtrl.Instance.GetTimeShootOneTime();
    protected override int GetNumberOfActack() => MiniBossNairanCtrl.Instance.GetNumberOfAttacksFollowAndShootBehaviour();
    protected override void SetProjectile(bool isOn) => MiniBossNairanCtrl.Instance.MinibossNairanShootNormal.SetIsFiring(isOn);
    protected override void UnSetAnimation() => MiniBossNairanCtrl.Instance.SetIsFollowAndShoot(false);
    
    protected override float GetRandomYValue() => Random.Range(-1.5f, 1f);
}
