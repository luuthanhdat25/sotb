using UnityEngine;

namespace Damage
{
    public class ItemMagnet : RepeatMonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent<Item>(out Item item))
            {
                item.MoveItemToPlayer();
            }
        }
    }
}