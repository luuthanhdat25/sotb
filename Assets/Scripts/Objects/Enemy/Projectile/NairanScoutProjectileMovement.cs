using Projectile;
using UnityEngine;

namespace Objects.Enemy.Normal_Enemy.AttackEnemy
{
    public class NairanScoutProjectileMovement : EnemyProjectilesMovement
    {
        [SerializeField] private float rateIncreaseSpeed = 0.01f;
        
        protected override void FixedUpdate()
        {
            abstractMoveSpeed += rateIncreaseSpeed;
            base.FixedUpdate();
        }
    }
}