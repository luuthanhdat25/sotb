using System.Collections;
using System.Collections.Generic;
using Enemy.Boss.Nairan.Miniboss;
using Player;
using UnityEngine;

public class MiniBossNairanLazerSlideBehaviour : StateMachineBehaviour
{
    [SerializeField] protected float timeDelayBeforeSlide;
    [SerializeField] protected float speedSlide;
    [SerializeField] protected float speedFollow;
    private Vector3 targetPosition = Vector3.zero;
    private float timer;
    private bool isStopTimer;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        targetPosition = GetTargetPosition(PlayerCtrl.Instance.GetCurrentPosition(), animator.transform.position);
        speedSlide = GetSpeedSlide();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        this.Behaviour(animator);
    }

    private void Behaviour(Animator animator)
    {
        if (animator.transform.position != targetPosition)
        {
            animator.transform.position = Vector3.MoveTowards(animator.transform.position, targetPosition,
                speedFollow * Time.deltaTime);
            if (animator.transform.position == targetPosition)
            {
                //SetProjectile(true);
                isStopTimer = false;
            }
        }
        else
        {
            if (!isStopTimer) timer += Time.deltaTime;
            /*if (!isStopTimer && timer >= timeShootInOneTime)
            {
                SetProjectile(false);
                targetPosition = GetTargetPosition(PlayerCtrl.Instance.GetCurrentPosition(), animator.transform.position);
                timer = 0;
                isStopTimer = true;
                numberOfAttacks--;
            }*/
        }
        
        /*else
        {
            SetProjectile(false);
            //UnSetAnimation();
        }*/
    }

    protected virtual Vector3 GetTargetPosition(Vector3 playerPos, Vector3 currentPos)
    {
        return new Vector3(playerPos.x, this.GetRandomYValue() + currentPos.y, playerPos.z);
    }

    protected virtual float GetRandomYValue() => Random.Range(-2f, 2f);

    protected float GetSpeedSlide() => MiniBossNairanCtrl.Instance.GetSpeedSlide();
    protected float GetSpeedFollow() => MiniBossNairanCtrl.Instance.GetSpeedFollow();
    protected float GetTimeDelayBeforeSlide() => MiniBossNairanCtrl.Instance.GetTimeDelayBeforeSlide();
    //protected void SetProjectile(bool isOn) => MiniBossNairanCtrl.Instance.MinibossNairanShootLazer.SpawnLazer(isOn);
    //protected  void UnSetAnimation();
}
