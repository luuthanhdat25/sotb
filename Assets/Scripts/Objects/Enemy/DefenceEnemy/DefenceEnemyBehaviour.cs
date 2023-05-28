using Player;
using UnityEngine;

namespace Enemy.DefenceEnemy
{
    public class DefenceEnemyBehaviour : EnemyBehaviour
    {
        [SerializeField] private float speedAttack = 8f;
        [SerializeField] private float rateOutSpeed = 1;
        
        private Vector2 targetPosition;
        private Vector2 outVector;
        private bool isOut;
        
        private void Start()
        {
            this.isOut = false;
        }
        
        protected override void Behaviour()
        {
            CaculateDirectionMove();
            if (targetPosition == Vector2.zero) return;
            Debug.DrawLine(transform.parent.position, targetPosition, Color.red);
            Move();
        }

        private void CaculateDirectionMove()
        {
            if (this.targetPosition != Vector2.zero || PlayerCtrl.Instance == null) return;
            this.targetPosition = PlayerCtrl.Instance.GetCurrentPosition();
        }
        
        private void Move()
        {
            float realSpeed = speedAttack * Time.fixedDeltaTime;
            Debug.DrawLine(transform.parent.position, targetPosition, Color.red);
            if(isOut == false)
            {
                transform.parent.position = Vector2.MoveTowards(transform.parent.position,
                    targetPosition, realSpeed);
                
                if ((Vector2)transform.parent.position == targetPosition)
                    isOut = true;
            }

            if (isOut == false) return;
            CaculateOutMove();
            if (this.outVector == Vector2.zero) return;

            transform.parent.position = Vector2.MoveTowards(transform.parent.position,
                    outVector, realSpeed * rateOutSpeed);
        }

        private void CaculateOutMove()
        {
            if (this.outVector != Vector2.zero) return;
            float getRandomX = UnityEngine.Random.Range(-15, 15);
            float getRandomY = UnityEngine.Random.Range(-10, -20);
            outVector = new Vector2(getRandomX, getRandomY).normalized * 15;
        }
    }
}