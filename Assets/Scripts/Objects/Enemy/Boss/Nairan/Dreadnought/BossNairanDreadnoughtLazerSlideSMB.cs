using Enemy;
using Enemy.Boss.Nairan.Miniboss.Boss;
using Enemy.Boss.Nairan.Miniboss.Boss.Battlecruiser;
using UnityEngine;

namespace Objects.Enemy.Boss.Nairan.Dreadnought
{
    public class BossNairanDreadnoughtLazerSlideSMB : AbsLazerSlideSMB
    {
        [SerializeField] private float yDistanceFromDefaut = 2;
    
        protected override Vector3 GetTargetPosition(Vector3 currentPos)
            => new Vector3(currentPos.x, currentPos.y + yDistanceFromDefaut, currentPos.z);

        protected override Vector3 GetCameraPosition()
            => BossNairanDreadnoughtCtrl.Instance.GetCameraPosition();

        protected override void ResetOutVector(Animator animator)
        {
            if (this.outVector != Vector3.zero) return;
            float dirY = animator.transform.position.y;
            outVector = new Vector3(-distanceLimit, dirY);
        }

        protected override float GetSpeedSlide()
            => DoubleBossNairanCtrl.Instance.SpeedSlide;

        protected override float GetSpeedFollow()
            => DoubleBossNairanCtrl.Instance.SpeedFollowSWBattlecruiser;

        protected override float GetTimeDelayBeforeSlide()
            => DoubleBossNairanCtrl.Instance.TimeDelayBeforeSlide;

        protected override void SetProjectile(bool isOn)
        {
            if(isOn) BossNairanDreadnoughtCtrl.Instance.BossShootLazer.SpawnLazerAfterTime(0);
            else BossNairanDreadnoughtCtrl.Instance.BossShootLazer.DespawnLazer();
        }

        protected override void UnSetAnimation()
        {
            //DoubleBossNairanCtrl.Instance.SwapDefaultPosition();
            BossNairanDreadnoughtCtrl.Instance.SetIsFinishBehaviour(true);
            if (BossNairanBattlecruiserCtrl.Instance.IsFinishBehaviour)
            {
                BossNairanDreadnoughtCtrl.Instance.SetIsFinishBehaviour(false);
                BossNairanDreadnoughtCtrl.Instance.SetIsLazerSlide(false);
            }
        }
    }
}