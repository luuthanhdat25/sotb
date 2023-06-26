using Enemy.Boss.Nairan.Miniboss.Boss;
using UnityEngine;

namespace Objects.Enemy.Boss.Nairan.Dreadnought
{
    public class BossNairanDreadnoughtIdle2SMB : AbsIdleSMB
    {
        protected override void ChangeState(Animator animator)
        {
            int randomState = DoubleBossNairanCtrl.Instance.GetRandomState(3);
            switch (randomState)
            {
                case 1: BossNairanDreadnoughtCtrl.Instance.SetIsSpawnTorpedo(true); break;
                case 2: BossNairanDreadnoughtCtrl.Instance.SetIsArcShootNormal(true); break;
                case 3: BossNairanDreadnoughtCtrl.Instance.SetIsTeleportAndShootLazer(true); break;
            }
        }

        protected override Vector3 GetDefaultPosition()
            => BossNairanDreadnoughtCtrl.Instance.GetDefaultPosition();
    
        protected override float GetTimeWait()
            => DoubleBossNairanCtrl.Instance.TimeWaitIdleTwo;
    }
}