using Damage.RhythmScripts;
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
            AudioSpawner.Instance.SpawnPlayerEffect(AudioSpawner.SoundEffectEnum.UpgradeItem);
            ItemDropSpawner.Instance.Despawn(transform.parent);
        }
    }
}