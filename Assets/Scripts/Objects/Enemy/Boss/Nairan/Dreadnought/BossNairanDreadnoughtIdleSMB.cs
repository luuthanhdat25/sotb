using Enemy.Boss.Nairan.Miniboss.Boss;
using UnityEngine;

namespace Objects.Enemy.Boss.Nairan.Dreadnought
{
    public class BossNairanDreadnoughtIdleSMB : AbsIdleSMB
    {
        protected override void ChangeState(Animator animator)
        {
            //if (DoubleBossNairanCtrl.Instance.CanGetStateNumber == false) return;
            //DoubleBossNairanCtrl.Instance.SetCanGetStateNumber(false);
            if (DoubleBossNairanCtrl.Instance.StateNumber == 1)
                BossNairanDreadnoughtCtrl.Instance.SetIsLazerSlide(true);
            else
                BossNairanDreadnoughtCtrl.Instance.SetIsFollowAndShootNormal(true);
        }

        protected override Vector3 GetDefaultPosition()
            => BossNairanDreadnoughtCtrl.Instance.GetDefaultPosition();
    
        protected override float GetTimeWait()
            => DoubleBossNairanCtrl.Instance.TimeWaitIdleOne;
    }
}