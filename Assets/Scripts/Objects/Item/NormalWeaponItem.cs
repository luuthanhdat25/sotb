using Player;
using UnityEngine;

namespace Damage
{
    public class NormalWeaponItem : Item
    {
        public override void UseItem()
        {
            base.UseItem();
            PlayerCtrl.Instance.PlayerAnimations.TurnOnNormalWeapon();
            PlayerCtrl.Instance.PlayerShoot.SetIsHasWeapon(true);
            //PlayMusic
            ItemDropSpawner.Instance.Despawn(transform.parent);
        }
    }
}