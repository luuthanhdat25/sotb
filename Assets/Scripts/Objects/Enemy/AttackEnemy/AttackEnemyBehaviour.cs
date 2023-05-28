using System;
using System.Collections;
using Enemy;
using UnityEngine;

namespace Objects.Enemy.AttackEnemy
{
    public class AttackEnemyBehaviour : EnemyBehaviour
    {
        [SerializeField] private float timeGoDown = 2f;
        [SerializeField] private int numberXVectorDown = 10;
        private float timer = 0;
        private bool isRight = true;

        protected override void Behaviour()
        {
            float realSpeed = pathfinder.waveSpawner.GetMoveSpeed() * Time.fixedDeltaTime;
            timer += Time.fixedDeltaTime;
            if (timer > timeGoDown)
            {
                transform.parent.position += CalculateVectorGodown() * realSpeed;
                timer = 0;
            }
        }

        private Vector3 CalculateVectorGodown()
        {
            Vector3 vectorGoDown = default;
            if (this.isRight)
            {
                vectorGoDown = (Vector3.down + Vector3.right) * 1.5f * numberXVectorDown;
                this.isRight = false;
            }
            else
            {
                vectorGoDown = (Vector3.down + Vector3.left) * 1.5f * numberXVectorDown;
                this.isRight = true;
            } 

            return vectorGoDown;
        }
    }
}