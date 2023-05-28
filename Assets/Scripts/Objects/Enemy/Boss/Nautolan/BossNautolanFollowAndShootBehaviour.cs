using Enemy;

public class BossNautolanFollowAndShootBehaviour : FollowAndShootStateMachineBehaviour
{
    protected override float GetSpeedFollow() => BossNautolanCtrl.Instance.GetSpeedFollow();
    protected override float GetTimeShootOneTime() => BossNautolanCtrl.Instance.GetTimeShootOneTime();
    protected override int GetNumberOfActack() => BossNautolanCtrl.Instance.GetNumberOfAttacksFollowAndShoot();
    protected override void SetProjectile(bool isOn) => BossNautolanCtrl.Instance.BossNautolanSpinningBulletShoot.SetIsFiring(isOn);
    protected override void UnSetAnimation() => BossNautolanCtrl.Instance.SetIsFollowAndShoot(false);
}
