using System;
using UnityEngine;

namespace Damage
{
    public class ItemMagnet : RepeatMonoBehaviour
    {
        private CircleCollider2D circleCollider;
        private void Start() => circleCollider ??= GetComponent<CircleCollider2D>();

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