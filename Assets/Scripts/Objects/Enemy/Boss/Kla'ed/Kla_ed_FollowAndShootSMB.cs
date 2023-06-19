using Enemy;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

public class Kla_ed_FollowAndShootSMB : AbsFollowAndShootSMB
{
    protected override void Behaviour(Animator animator)
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
                }
            }
            else
            {
                timer += Time.deltaTime;
                if (timer >= timeShootInOneTime)
                {
                    SetProjectile(false);
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
    
    private float GetRandomXValue()
    {
        float randomValue = Random.Range(-0.8f, -0.4f);
        if (Random.Range(0, 2) == 0)
            randomValue *= -1;
        return randomValue;
    }
    
    protected override float GetRandomYValue() => Random.Range(-2f, 1f);
    protected override float GetSpeedFollow() => MiniBossKla_edCtrl.Instance.GetSpeedFollow();

    protected override float GetTimeShootOneTime() => MiniBossKla_edCtrl.Instance.GetTimeShootOneTime();

    protected override int GetNumberOfActack() => MiniBossKla_edCtrl.Instance.GetNumberOfAttacksFollowAndShootBehaviour();

    protected override void SetProjectile(bool isOn) => MiniBossKla_edCtrl.Instance.MinibossKla_ed_NormalShoot.SetIsFiring(isOn);
    
    protected override void UnSetAnimation() => MiniBossKla_edCtrl.Instance.SetIsFollowAndShoot(false);
}
