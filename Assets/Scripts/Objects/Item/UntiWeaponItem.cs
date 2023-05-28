using Player;
using UnityEngine;

namespace Damage
{
    public class UntiWeaponItem : Item
    {
        public override void UseItem()
        {
            base.UseItem();
            PlayerCtrl.Instance.PlayerAnimations.TurnOnUntiWeapon();
            PlayerCtrl.Instance.PlayerUnti.SetIsHasUntiWeaponTrue();
            //PlayMusic
            ItemDropSpawner.Instance.Despawn(transform.parent);
        }
    }
}