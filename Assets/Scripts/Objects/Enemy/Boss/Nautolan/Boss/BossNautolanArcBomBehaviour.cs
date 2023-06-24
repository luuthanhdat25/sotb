using Enemy;
using UnityEngine;

public class BossNautolanArcBomBehaviour : AbsArcShootSMB
{
    protected override Vector3 GetStartPosition() => BossNautolanCtrl.Instance.GetEndPosition();
    
    protected override Vector3 GetEndPosition() => BossNautolanCtrl.Instance.GetStartPosition();
    
    protected override float GetCurveHeight() => BossNautolanCtrl.Instance.GetCurveHeight();

    protected override float GetSpeedArc() => BossNautolanCtrl.Instance.GetSpeedArc();

    protected override float GetSpeedToStartPosition() => BossNautolanCtrl.Instance.GetSpeedToStartPosition();

    protected override void SetProjectile(bool isOn)
    { 
        BossNautolanCtrl.Instance.BossNautolanBomShoot.SetIsFiring(isOn);
        BossNautolanCtrl.Instance.BossNautolanModelShipAnimation.SetIsBomDrop(isOn);
    }
    
    protected override void UnSetAniamtion() => BossNautolanCtrl.Instance.SetIsBomDrop(false);
}
