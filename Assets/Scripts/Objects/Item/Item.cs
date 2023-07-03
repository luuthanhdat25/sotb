using System;
using System.Net.NetworkInformation;
using DefaultNamespace;
using Player;
using UnityEngine;
using UnityEngine.UIElements;

namespace Damage
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private float moveToTargetSpeed = 10f;
        private bool isMoveToTarget = false;
        
        public virtual void MoveItemToPlayer()
        {
            this.isMoveToTarget = true;
        }

        private void OnDisable()
        {
            this.isMoveToTarget = false;
        }

        public virtual void UseItem()
        {
            //For override
        }
        
        private void FixedUpdate()
        {
            this.MoveToTargetWhenInteract();
        }

        private void MoveToTargetWhenInteract()
        {
            if (isMoveToTarget && PlayerCtrl.Instance.GetIsPlayerDead() && !GameManager.Instance.IsFinishGame()) isMoveToTarget = false;
            if (!isMoveToTarget || PlayerCtrl.Instance.GetIsPlayerDead()) return;
            Vector3 directionToMove = (PlayerCtrl.Instance.GetCurrentPosition() - transform.parent.position).normalized;
            transform.parent.Translate(directionToMove * moveToTargetSpeed * Time.fixedDeltaTime);
        }
    }
}