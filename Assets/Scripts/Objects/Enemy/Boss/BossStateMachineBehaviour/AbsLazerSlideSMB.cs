using Enemy.Boss.Nairan.Miniboss;
using Player;
using UnityEngine;

namespace Enemy
{
    public abstract class AbsLazerSlideSMB : StateMachineBehaviour
    {
        [SerializeField] protected float distanceLimit = 14f;
        protected float timeDelayBeforeSlide;
        protected float speedSlide;
        protected float speedFollow;
        protected Vector3 outVector;
        private const float distanceReset = 8f;
        private Vector3 targetPosition = Vector3.zero;
        private float timer;
        private bool isStopTimer = true;
        private float currentDistance = 0f;
        private bool isSlideGoOut = false;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            targetPosition = GetTargetPosition(animator.transform.position);
            speedSlide = GetSpeedSlide();
            timeDelayBeforeSlide = GetTimeDelayBeforeSlide();
            speedFollow = GetSpeedFollow();
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            this.Behaviour(animator);
        }

        private void Behaviour(Animator animator)
        {
            if (!isSlideGoOut && !IsGoOutScreen(animator))
            {
                if (animator.transform.position != targetPosition && isStopTimer)
                {
                    //Move To Ready Position
                    MoveTowardsTo(targetPosition, speedFollow, animator);
                    if (animator.transform.position == targetPosition) isStopTimer = false;
                }
                else
                {
                    //Wait for time Delay Before Slide
                    if (!isStopTimer) timer += Time.deltaTime;
                    if (!isStopTimer && timer >= timeDelayBeforeSlide)
                    {
                        //Slide to Player
                        SetProjectile(true);
                        ResetOutVector(animator);
                        MoveTowardsTo(outVector, speedSlide, animator);
                    }
                }
            }
            else
            {
                if (!isSlideGoOut) TransformToContraryDirectMove(animator);
                isSlideGoOut = true;
                MoveTowardsTo(targetPosition, speedSlide, animator);
                if (animator.transform.position == targetPosition)
                {
                    timer = 0;
                    isStopTimer = true;
                    outVector = Vector3.zero;
                    isSlideGoOut = false;
                    SetProjectile(false);
                    UnSetAnimation();
                }
            }
        }

        private void MoveTowardsTo(Vector3 targetVector3, float speed, Animator animator)
        {
            animator.transform.position = Vector3.MoveTowards(animator.transform.position,
                targetVector3, speed * Time.deltaTime);
        }

        private bool IsGoOutScreen(Animator animator)
        {
            this.currentDistance = Vector3.Distance(animator.transform.position, GetCameraPosition());
            if (this.currentDistance > distanceLimit) return true;
            return false;
        }

        protected abstract Vector3 GetCameraPosition();

        protected abstract void ResetOutVector(Animator animator);

        private void TransformToContraryDirectMove(Animator animator)
        {
            if (outVector.x >= 0)
                animator.transform.position = new Vector3(-distanceReset, animator.transform.position.y,
                    animator.transform.position.z);
            else
                animator.transform.position = new Vector3(distanceReset, animator.transform.position.y,
                    animator.transform.position.z);
        }

        protected virtual Vector3 GetTargetPosition(Vector3 currentPos)
        {
            return new Vector3(GetPlayerPosition().x, this.GetRandomYValue() + currentPos.y, GetPlayerPosition().z);
        }

        protected Vector3 GetPlayerPosition() => PlayerCtrl.Instance.GetCurrentPosition();

        protected virtual float GetRandomYValue() => Random.Range(-2f, 2f);

        protected abstract float GetSpeedSlide();
        protected abstract float GetSpeedFollow();
        protected abstract float GetTimeDelayBeforeSlide();

        protected abstract void SetProjectile(bool isOn);
        protected abstract void UnSetAnimation();
    }
}