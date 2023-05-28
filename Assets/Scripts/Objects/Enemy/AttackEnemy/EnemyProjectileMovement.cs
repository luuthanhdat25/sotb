using Others;
using UnityEngine;

namespace Projectile
{
    public class EnemyProjectileMovement : ObjectMovement
    {
        [SerializeField] protected float moveSpeed;
        protected override void ChangeSpeed()
        {
            abstractMoveSpeed = this.moveSpeed;
        }

        protected override void ChangeDirection()
        {
            this.abstractDirection = Vector2.down;
        }
    }
}