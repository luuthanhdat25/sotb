using Player;
using UnityEngine;
using Random = UnityEngine.Random;

public class Kla_ed_FollowAndShootBehaviour : StateMachineBehaviour
{
    [SerializeField] private float timeShootInOneTime;
    [SerializeField] private float speedFollow;
    private Vector3 targetPosition = Vector3.zero;
    private int numberOfAttacks;
    private float timer;
    private bool isStopTimer;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        speedFollow = MiniBossKla_edCtrl.Instance.GetSpeedFollow();
        timeShootInOneTime = MiniBossKla_edCtrl.Instance.GetTimeShootOneTime();
        numberOfAttacks = MiniBossKla_edCtrl.Instance.GetNumberOfAttacksFollowAndShootBehaviour();
        targetPosition = GetTargetPosition(PlayerCtrl.Instance.GetCurrentPosition(), animator.transform.position);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
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
                    MiniBossKla_edCtrl.Instance.MinibossKla_ed_NormalShoot.SetIsFiring(true);
                    isStopTimer = false;
                }
            }
            else
            {
                if (!isStopTimer) timer += Time.deltaTime;
                if (!isStopTimer && timer >= timeShootInOneTime)
                {
                    MiniBossKla_edCtrl.Instance.MinibossKla_ed_NormalShoot.SetIsFiring(false);
                    targetPosition = GetTargetPosition(PlayerCtrl.Instance.GetCurrentPosition(), animator.transform.position);
                    timer = 0;
                    isStopTimer = true;
                    numberOfAttacks--;
                }
            }
        }
        else
        {
            MiniBossKla_edCtrl.Instance.MinibossKla_ed_NormalShoot.SetIsFiring(false);
            MiniBossKla_edCtrl.Instance.SetIsFollowAndShoot(false);
        }
    }

    private Vector3 GetTargetPosition(Vector3 playerPos, Vector3 currentPos)
    {
        return new Vector3(playerPos.x, this.GetRandomYValue() + currentPos.y, playerPos.z);
    }

    private float GetRandomYValue() => Random.Range(-2f, 2f);
}
