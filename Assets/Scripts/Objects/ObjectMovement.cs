using UnityEngine;

namespace Others
{
    public abstract class ObjectMovement : RepeatMonoBehaviour
    {
        protected float abstractMoveSpeed;
        protected Vector2 abstractDirection;

        protected virtual void OnEnable()
        {
            ResetValueWhenEnableAgain();
        }

        private void ResetValueWhenEnableAgain()
        {
            ChangeSpeed();
            ChangeDirection();
        }

        protected abstract void ChangeSpeed();
        protected abstract void ChangeDirection();
        
        protected virtual void FixedUpdate()
        {
            transform.parent.Translate(abstractDirection * abstractMoveSpeed * Time.fixedDeltaTime);
        }
    }
}