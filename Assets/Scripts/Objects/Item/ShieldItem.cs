using Player;
using UnityEngine;

namespace Damage
{
    public class ShieldItem : Item
    {
        [SerializeField] private float timeEffective = 2f;
        public override void UseItem()
        {
            base.UseItem();
            PlayerCtrl.Instance.PlayerAnimations.TurnOnShield();
            PlayerCtrl.Instance.PlayerDamageSender.SetIsShieldUp(true);
            //PlayMusic
            //PlayVFX
            //PlayUIBar
            ItemDropSpawner.Instance.Despawn(transform.parent);
        }
    }
}