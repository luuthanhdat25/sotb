using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyBehaviour : RepeatMonoBehaviour
    {
        [SerializeField] protected Pathfinder pathfinder;
        [SerializeField] protected Transform defaultPosition;
        [SerializeField] protected float timeWaiting;
        
        protected bool isBehaviour = false;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadPathfinder();
        }

        protected virtual void LoadPathfinder()
        {
            if (this.pathfinder != null) return;
            this.pathfinder = transform.parent.GetComponent<Pathfinder>();
        }
        
        protected virtual void FixedUpdate()
        {
            if (this.pathfinder == null) return;

            if (!isBehaviour)
            {
                MoveToDefaultPosition();
                if (transform.parent.position == defaultPosition.position) 
                    StartCoroutine(this.WaitBehaviour());
            }
            else Behaviour();
        }

        protected virtual void MoveToDefaultPosition()
        {
            float realSpeed = pathfinder.waveSpawner.GetMoveSpeed() * Time.fixedDeltaTime;
            transform.parent.position =
                Vector2.MoveTowards(transform.parent.position, this.defaultPosition.position, realSpeed);
            
            Debug.DrawLine(transform.parent.position, this.defaultPosition.position, Color.red);
        }

        private IEnumerator WaitBehaviour()
        {
            yield return new WaitForSeconds(this.timeWaiting);
            isBehaviour = true;
        }
        
        protected virtual void Behaviour()
        {
            //For override
        }
    }
}