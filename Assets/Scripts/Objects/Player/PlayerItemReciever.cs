using Damage;
using UnityEngine;

namespace Player
{
    public class PlayerItemReciever : MonoBehaviour
    {
        /// <summary>
        /// This class add to collider of damage send/recive
        /// </summary>
        /// <param name="col"></param>
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent<Item>(out Item item))
            {
                item.UseItem();
            }
        }
    }
}