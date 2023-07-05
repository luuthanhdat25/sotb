using Damage.RhythmScripts;
using Objects.UI.HUD;
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
            PlayerCtrl.Instance.PlayerAnimations.FullHealth();
            PlayerCtrl.Instance.ItemMagnet.SetRadiusItemMagnet(1);
            UIManager.Instance.SetActiveScoreHUD(true);
            UIManager.Instance.SetActiveTimerHUD(true);
            AudioSpawner.Instance.SpawnPlayerEffect(AudioSpawner.SoundEffectEnum.UpgradeItem);
            ItemDropSpawner.Instance.Despawn(transform.parent);
        }
    }
}