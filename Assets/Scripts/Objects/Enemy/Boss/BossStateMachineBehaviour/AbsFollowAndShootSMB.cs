using Player;
using UnityEngine;

namespace Enemy
{
    public abstract class AbsFollowAndShootSMB : StateMachineBehaviour
    {
        [SerializeField] protected float timeShootInOneTime;
        [SerializeField] protected float speedFollow;
        [SerializeField] protected int numberOfAttacks;
        protected Vector3 targetPosition = Vector3.zero;
        protected float timer;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            targetPosition = GetTargetPosition(PlayerCtrl.Instance.GetCurrentPosition(), animator.transform.position);
            speedFollow = GetSpeedFollow();
            timeShootInOneTime = GetTimeShootOneTime();
            numberOfAttacks = GetNumberOfActack();
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            this.Behaviour(animator);
        }

        protected virtual void Behaviour(Animator animator)
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
                        targetPosition = GetTargetPosition(PlayerCtrl.Instance.GetCurrentPosition(), animator.transform.position);
                        timer = 0;
                        numberOfAttacks--;
                    }
                }
            }
            else
            {
                SetProjectile(false);
                UnSetAnimation();
            }
        }

        protected virtual Vector3 GetTargetPosition(Vector3 playerPos, Vector3 currentPos)
        {
            return new Vector3(playerPos.x, this.GetRandomYValue() + currentPos.y, playerPos.z);
        }

        protected virtual float GetRandomYValue() => Random.Range(-1.5f, 1.5f);

        protected abstract float GetSpeedFollow();
        protected abstract float GetTimeShootOneTime();
        protected abstract int GetNumberOfActack();
        protected abstract void SetProjectile(bool isOn);
        protected abstract void UnSetAnimation();
    }
}