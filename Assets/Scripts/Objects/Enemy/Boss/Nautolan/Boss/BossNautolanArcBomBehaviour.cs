using Enemy;
using UnityEngine;

public class BossNautolanArcBomBehaviour : ArcShootStateMachineBehaviour
{
    protected override Vector3 GetStartPosition() => BossNautolanCtrl.Instance.GetEndPosition();
    protected override Vector3 GetEndPosition() => BossNautolanCtrl.Instance.GetStartPosition();
    protected override void SetProjectile(bool isOn) => BossNautolanCtrl.Instance.BossNautolanBomShoot.SetIsFiring(isOn);
    protected override void UnSetAniamtion() => BossNautolanCtrl.Instance.SetIsBomDrop(false);
}
