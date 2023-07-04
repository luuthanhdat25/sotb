using System;
using DefaultNamespace;
using UnityEngine;

namespace Damage
{
    public class ItemMagnet : RepeatMonoBehaviour
    {
        [SerializeField] private float winGameRadius = 10;
        
        private CircleCollider2D circleCollider;
        
        private void Start()
        {
            LoadCircleCollider2D();
            SubcribeEvent();
        }

        private void LoadCircleCollider2D()
        {
            if (this.circleCollider != null) return; 
            this.circleCollider = GetComponent<CircleCollider2D>();
        }

        private void SubcribeEvent()
        {
            GameManager.Instance.WinGameEvent += 
                (object o, EventArgs e) => this.circleCollider.radius = winGameRadius;
        }

        public void SetRadiusItemMagnet(float radius) => this.circleCollider.radius = radius;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent<Item>(out Item item))
            {
                item.MoveItemToPlayer();
            }
        }
    }
}