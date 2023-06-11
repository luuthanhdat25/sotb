using UnityEngine;

namespace Objects.Enemy.Boss.Nairan.Dreadnought
{
    public class BossNairanDreadnoughtArcShootNormalSMB : StateMachineBehaviour
    {
        [SerializeField] protected float curveHeight = 2f;   
        [SerializeField] protected float speedArc;
        [SerializeField] protected float speedToStartPosition;
        private int numberLoop = 2;
        private float timer = 0f;       
        private Vector3 centerPoint;    
        private Vector3 startPosition;    
        private Vector3 endPosition; 
        private bool moveToStartPoint = true;
        
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            BossNairanDreadnoughtCtrl.Instance.BossNairanDreadnoughtShootNormal.SetFiringRate(BossNairanDreadnoughtCtrl.Instance.FiringRate);
            startPosition = GetStartPosition();
            endPosition = GetEndPosition();
            numberLoop = GetNumberLoop();
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
            if(numberLoop >= 0)
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
                    animator.transform.position = endPosition;
                    timer = 0;
                    SwapStartAndEndPosition();
                    numberLoop--;
                }
            }
            else
            {
                moveToStartPoint = true;
                SetProjectile(false);
                UnSetAniamtion();
            }
        }

        private void SwapStartAndEndPosition()
            => (startPosition, endPosition) = (endPosition, startPosition);
        

        protected void SetProjectile(bool isOn) 
            => BossNairanDreadnoughtCtrl.Instance.BossNairanDreadnoughtShootNormal.SetIsFiring(isOn);
        protected Vector3 GetStartPosition() => BossNairanDreadnoughtCtrl.Instance.GetStartPosition();
        protected  Vector3 GetEndPosition() => BossNairanDreadnoughtCtrl.Instance.GetEndPosition();
        protected void UnSetAniamtion() => BossNairanDreadnoughtCtrl.Instance.SetIsArcShootNormal(false);
        protected int GetNumberLoop() => BossNairanDreadnoughtCtrl.Instance.NumberOfLoop;
        protected float GetSpeedArc() =>  BossNairanDreadnoughtCtrl.Instance.SpeedArcShockWave;
        protected float GetSpeedToStartPosition() => BossNairanDreadnoughtCtrl.Instance.SpeedGoToReadyPosition;
    }
}