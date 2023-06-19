using Damage.RhythmScripts;
using Objects.UI.HUD;
using Player;

namespace Damage
{
    public class BurstEngineItem : Item
    {
        public override void UseItem()
        {
            base.UseItem();
            PlayerCtrl.Instance.PlayerAnimations.SetBurstEngineUpgrade();
            PlayerCtrl.Instance.PlayerMovement.SetCanMoveNormal(true);
            PlayerCtrl.Instance.PlayerMovement.SetCanUseDashToTrue();
            HUDManager.Instance.SetActiveBoostceilBar(true);
            AudioManager.Instance.SpawnPlayerEffect(AudioManager.SoundEffectEnum.UpgradeItem);
            ItemDropSpawner.Instance.Despawn(transform.parent);
        }
    }
}