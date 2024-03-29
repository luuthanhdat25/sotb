using Enemy;
using Enemy.Boss.Nairan.Miniboss.Boss;
using Enemy.Boss.Nairan.Miniboss.Boss.Battlecruiser;
using UnityEngine;

namespace Objects.Enemy.Boss.Nairan.Dreadnought
{
    public class BossNairanDreadnoughtFollowAndShootNormalSMB : AbsFollowAndShootSMB
    {
        
        protected override float GetRandomYValue() => Random.Range(-1.2f, -0.5f);
    
        protected override float GetSpeedFollow()
            => DoubleBossNairanCtrl.Instance.SpeedFollowNDreadnought;

        protected override float GetTimeShootOneTime()
            => DoubleBossNairanCtrl.Instance.TimeShootOneTimeNDreadnought;

        protected override int GetNumberOfActack()
            => DoubleBossNairanCtrl.Instance.NumOfShootAttackNDreadnought;

        protected override void SetProjectile(bool isOn)
        {
            BossNairanDreadnoughtCtrl.Instance.BossNairanDreadnoughtModelShipAnimation.SetIsFollowAndShootNormal(isOn);
            BossNairanDreadnoughtCtrl.Instance.BossNairanDreadnoughtShootNormal.SetIsFiring(isOn);
        }

        protected override void UnSetAnimation()
        {
            BossNairanDreadnoughtCtrl.Instance.SetIsFinishBehaviour(true);
            if (BossNairanBattlecruiserCtrl.Instance.IsFinishBehaviour && BossNairanBattlecruiserCtrl.Instance.IsInDefaultPosition())
            {
                BossNairanDreadnoughtCtrl.Instance.SetIsFollowAndShootNormal(false);
            }
        }
    }
}