using Enemy;
using UnityEngine;

public class BossNautolanArcShootBehaviour : ArcShootStateMachineBehaviour
{
    protected override Vector3 GetStartPosition() => BossNautolanCtrl.Instance.GetStartPosition();
    protected override Vector3 GetEndPosition() => BossNautolanCtrl.Instance.GetEndPosition();
    protected override void SetProjectile(bool isOn) => BossNautolanCtrl.Instance.MinibossNautolanShoot.SetIsFiring(isOn);
    protected override void UnSetAniamtion() => BossNautolanCtrl.Instance.SetIsArcShoot(false);
}







