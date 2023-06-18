using Player;
using UnityEngine;

namespace Enemy
{
    public abstract class AbsChasePlayerSMB: StateMachineBehaviour
    {
        [SerializeField] protected float rateOfIncreaseSpeed = 1.5f;
        [SerializeField] protected float distanceLimit = 14f;
        protected Vector3 targetPosition = Vector3.zero;
        protected float speedChase;
        protected bool isGoOverPlayer;
        protected int numberOfAttacks;
        private float currentDistance = 0f;
        protected Vector3 outVector;
        public override  void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            isGoOverPlayer = false;
            numberOfAttacks = GetNumberOfAttacks();
            speedChase = GetSpeedChase();
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (numberOfAttacks != 0) Behaviour(animator);
            else
            {
                SetShipModelAnimationChase(false);
                UnSetAnimation();
            }
        }
        
        protected virtual void Behaviour(Animator animator)
        {
            AssignTargetPositionIfEqualVectorZero();
            MoveToTarget(animator);
        }

        protected virtual void AssignTargetPositionIfEqualVectorZero()
        {
            if (this.targetPosition != Vector3.zero) return;
            this.targetPosition = PlayerCtrl.Instance.GetCurrentPosition();
        }
        
        protected virtual void MoveToTarget(Animator animator)
        {
            SetShipModelAnimationChase(true);
            if (!IsGoOutScreen(animator))
            {
                if(!isGoOverPlayer)
                {
                    MoveTowardsTo(targetPosition, speedChase, animator);
                    if (animator.transform.position == targetPosition)
                        isGoOverPlayer = true;
                }
                else
                {
                    ResetOutVector();
                    MoveTowardsTo(outVector, speedChase * rateOfIncreaseSpeed, animator);
                }
            }
            else
            {
                numberOfAttacks--;
                isGoOverPlayer = false;
                ResetTargetPosition();
                TransformToTopScreen(animator);
                outVector = Vector3.zero;
            }
        }

        protected virtual  void MoveTowardsTo(Vector3 vectorMove, float speed, Animator animator)
        {
            animator.transform.position = Vector3.MoveTowards(animator.transform.position,
                vectorMove, speed * Time.deltaTime);
        }

        protected virtual void ResetOutVector()
        {
            if (this.outVector != Vector3.zero) return;
            float getRandomX = Random.Range(-10f, 10f);
            float getRandomY = Random.Range(-17f, -13f);
            this.outVector = new Vector3(getRandomX, getRandomY);
        }
        
        private bool IsGoOutScreen(Animator animator)
        {
            this.currentDistance = Vector3.Distance(animator.transform.position, GetCameraPosition());
            if (this.currentDistance > distanceLimit) return true;
            return false;
        }
        
        private void ResetTargetPosition() => this.targetPosition = PlayerCtrl.Instance.GetCurrentPosition();
        
        protected virtual void TransformToTopScreen(Animator animator)
        {
            if (outVector.x >= 0) animator.transform.position = new Vector3(-outVector.x, 9.5f, outVector.z);
            else animator.transform.position = new Vector3(-outVector.x, 9.5f, outVector.z);
        }
        
        
        protected abstract int GetNumberOfAttacks();
        protected abstract float GetSpeedChase();
        protected abstract void SetShipModelAnimationChase(bool isOn);
        protected abstract void UnSetAnimation();
        protected abstract Vector3 GetCameraPosition();
    }
}