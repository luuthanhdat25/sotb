using Others;
using UnityEngine;

namespace Damage
{
    public class ItemMovement : ObjectMovement
    {
        [SerializeField] protected float speedMax = 6f;
        [SerializeField] protected float speedMin = 1f;

        protected override void ChangeSpeed()
        {
            abstractMoveSpeed = this.RandomizeSpeed();
        }
        
        private float RandomizeSpeed()
        {
            return Random.Range(speedMin, speedMax);
        }

        protected override void ChangeDirection()
        {
            this.abstractDirection = Vector2.down;
        }
    }
}