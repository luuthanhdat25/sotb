using Player;
using UnityEngine;

public class MiniBossNautolanFollowAndShootBehaviour : StateMachineBehaviour
{
    [SerializeField] protected float timeShootInOneTime; // 5
    [SerializeField] protected float speedFollow;
    protected Vector3 targetPosition = Vector3.zero;
    protected int numberOfAttacks; //1
    private float timer;
    private bool isStopTimer;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        speedFollow = MinibossNautolanCtrl.Instance.GetSpeedFollow();
        timeShootInOneTime = MinibossNautolanCtrl.Instance.GetTimeShootOneTime();
        numberOfAttacks = MinibossNautolanCtrl.Instance.GetNumberOfAttacksFollowAndShootBehaviour();
        targetPosition = GetTargetPosition(PlayerCtrl.Instance.GetCurrentPosition(), animator.transform.position);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        this.Behaviour(animator);
    }

    private void Behaviour(Animator animator)
    {
        if (numberOfAttacks != 0)
        {
            if (animator.transform.position != targetPosition)
            {
                animator.transform.position = Vector3.MoveTowards(animator.transform.position, targetPosition,
                    speedFollow * Time.deltaTime);
                if (animator.transform.position == targetPosition)
                {
                    SetProjectile(true);
                    isStopTimer = false;
                }
            }
            else
            {
                if (!isStopTimer) timer += Time.deltaTime;
                if (!isStopTimer && timer >= timeShootInOneTime)
                {
                    SetProjectile(false);
                    targetPosition = GetTargetPosition(PlayerCtrl.Instance.GetCurrentPosition(), animator.transform.position);
                    timer = 0;
                    isStopTimer = true;
                    numberOfAttacks--;
                }
            }
        }
        else
        {
            SetProjectile(false);
            UnSetAniamtion();
        }
    }

    protected Vector3 GetTargetPosition(Vector3 playerPos, Vector3 currentPos)
    {
        return new Vector3(playerPos.x, this.GetRandomYValue() + currentPos.y, playerPos.z);
    }

    protected virtual float GetRandomYValue() => Random.Range(-2.5f, 2f);
    
    protected virtual void SetProjectile(bool isOn)
    {
        MinibossNautolanCtrl.Instance.MinibossNautolanShoot.SetIsFiring(isOn);
    }

    protected virtual void UnSetAniamtion()
    {
        MinibossNautolanCtrl.Instance.SetIsFollowAndShoot(false);
    }
}
