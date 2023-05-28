using Player;
using UnityEngine;

namespace Damage
{
    public class DefaultEngineItem : Item
    {
        public override void UseItem()
        {
            base.UseItem();
            PlayerCtrl.Instance.PlayerAnimations.TurnOnDefaultEngine();
            PlayerCtrl.Instance.PlayerMovement.SetCanMoveNormal(true);
            //PlayMusic
            ItemDropSpawner.Instance.Despawn(transform.parent);
        }
    }
}