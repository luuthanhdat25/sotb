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
                }
            }
            else
            {
                timer += Time.deltaTime;
                if ( timer >= timeShootInOneTime)
                {
                    MiniBossKla_edCtrl.Instance.MinibossKla_ed_NormalShoot.SetIsFiring(false);
                    targetPosition = GetNewTargetPosition(animator);
                    timer = 0;
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

    private Vector3 GetNewTargetPosition(Animator animator)
    {
        Vector3 newTargetPosition = GetTargetPosition(PlayerCtrl.Instance.GetCurrentPosition(), animator.transform.position);
        if (newTargetPosition.x == targetPosition.x)
            return new Vector3(PlayerCtrl.Instance.GetCurrentPosition().x + GetRandomXValue(),
                this.GetRandomYValue() + animator.transform.position.y, animator.transform.position.z);
        return newTargetPosition;
    }

    private Vector3 GetTargetPosition(Vector3 playerPos, Vector3 currentPos)
    {
        return new Vector3(playerPos.x, this.GetRandomYValue() + currentPos.y, playerPos.z);
    }

    private float GetRandomYValue() => Random.Range(-2f, 1f);
    private float GetRandomXValue()
    {
        float randomValue = Random.Range(-0.8f, -0.4f);
        if (Random.Range(0, 2) == 0)
            randomValue *= -1;
        return randomValue;
    }
}
