using Enemy.Boss.Nairan.Miniboss.Boss;
using Enemy.Boss.Nairan.Miniboss.Boss.Battlecruiser;
using UnityEngine;

namespace Objects.Enemy.Boss.Nairan.Dreadnought
{
    public class BossNairanDreadnoughtIdleSMB : AbsIdleSMB
    {
        private bool isReady = false;
        protected override void Behaviour(Animator animator)
        {
            if (animator.transform.position != GetDefaultPosition() && !isReady)
            {
                MoveToDefaultPosition(animator, GetDefaultPosition());
                if (animator.transform.position == GetDefaultPosition())
                {
                    isReady = true;
                    BossNairanDreadnoughtCtrl.Instance.SetIsFinishBehaviour(false);
                }
            }
            else
            {
                if (BossNairanDreadnoughtCtrl.Instance.IsInDefaultPosition() == false) return;
                timer += Time.deltaTime;
                if (timer >= timeWait)
                {
                    this.ChangeState(animator);
                }
            }
        }
        
        
        
        protected override void ChangeState(Animator animator)
        {
            if (BossNairanBattlecruiserCtrl.Instance.IsInDefaultPosition() == false) return;
            if (DoubleBossNairanCtrl.Instance.StateNumber == 1)
            {
                isReady = false;
                timer = 0;
                BossNairanDreadnoughtCtrl.Instance.SetIsLazerSlide(true);
            }
            else if (DoubleBossNairanCtrl.Instance.StateNumber == 2)
            {
                isReady = false;
                timer = 0;
                BossNairanDreadnoughtCtrl.Instance.SetIsFollowAndShootNormal(true);
            }
        }

        protected override Vector3 GetDefaultPosition()
            => BossNairanDreadnoughtCtrl.Instance.GetDefaultPosition();
    
        protected override float GetTimeWait()
            => DoubleBossNairanCtrl.Instance.TimeWaitIdleOne;
    }
}