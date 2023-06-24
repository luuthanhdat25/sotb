using UnityEngine;

namespace Enemy
{
    public abstract class AbsArcShootSMB : StateMachineBehaviour
    {
        [SerializeField] protected float curveHeight = 3f;   
        [SerializeField] protected float speedArc = 0.6f;
        [SerializeField] protected float speedToStartPosition = 5;

        private float timer = 0f;       
        private Vector3 centerPoint;    
        private Vector3 startPosition;    
        private Vector3 endPosition; 
        private bool moveToStartPoint = true;
        
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            startPosition = GetStartPosition();
            endPosition = GetEndPosition();
            curveHeight = GetCurveHeight();
            speedArc = GetSpeedArc();
            speedToStartPosition = GetSpeedToStartPosition();
        }
        
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (moveToStartPoint)
                MoveToStartPoint(animator);
            else
                MoveAlongArc(animator);
        }

        protected virtual void MoveToStartPoint(Animator animator)
        {
            if (animator.transform.position != startPosition)
                animator.transform.position = Vector3.MoveTowards(animator.transform.position, startPosition, speedToStartPosition * Time.deltaTime);
            else
                moveToStartPoint = false;  
        }

        protected virtual void MoveAlongArc(Animator animator)
        {
            if (timer < 1f)
            {
                SetProjectile(true);
                Vector3 currentPos = Vector3.Lerp(startPosition, endPosition, timer);
                currentPos.y -= Mathf.Sin(timer * Mathf.PI) * curveHeight;
                animator.transform.position = currentPos;
                timer += Time.deltaTime * speedArc;
            }
            else
            {
                ReturnIdleBehaviour(animator);
            }
        }

        protected virtual void ReturnIdleBehaviour(Animator animator)
        {
            animator.transform.position = endPosition;
            timer = 0;
            moveToStartPoint = true;
            SetProjectile(false);
            UnSetAniamtion();
        }
        
        protected abstract Vector3 GetStartPosition();
        
        protected abstract Vector3 GetEndPosition();

        protected abstract float GetCurveHeight();
        
        protected abstract float GetSpeedArc();

        protected abstract float GetSpeedToStartPosition();
        
        protected abstract void SetProjectile(bool isOn);
        
        protected abstract void UnSetAniamtion();
    }
}