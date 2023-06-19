using Damage.RhythmScripts;
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
            AudioManager.Instance.SpawnPlayerEffect(AudioManager.SoundEffectEnum.UpgradeItem);
            ItemDropSpawner.Instance.Despawn(transform.parent);
        }
    }
}