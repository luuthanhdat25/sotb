using Enemy.Boss.Nairan.Miniboss.Boss;
using Enemy.Boss.Nairan.Miniboss.Boss.Battlecruiser;
using Objects.Enemy.Boss.Nairan.Dreadnought;
using UnityEngine;

public class BossNairanBattlecruiserIdleSMB : AbsIdleSMB
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
                BossNairanBattlecruiserCtrl.Instance.SetIsFinishBehaviour(false);
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
        if (BossNairanDreadnoughtCtrl.Instance.IsInDefaultPosition() == false) return;
        //DoubleBossNairanCtrl.Instance.SetCanGetStateNumber(false);
        
        if (DoubleBossNairanCtrl.Instance.StateNumber == 1)
        {
            isReady = false;
            timer = 0;
            BossNairanBattlecruiserCtrl.Instance.SetIsLazerSlide(true);
        }
        else if (DoubleBossNairanCtrl.Instance.StateNumber == 2)
        {
            isReady = false;
            timer = 0;
            BossNairanBattlecruiserCtrl.Instance.SetIsFollowAndShootShockWave(true);
        }
    }

    protected override Vector3 GetDefaultPosition()
        => BossNairanBattlecruiserCtrl.Instance.GetDefaultPosition();
    
    protected override float GetTimeWait()
        => DoubleBossNairanCtrl.Instance.TimeWaitIdleOne;
}
