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
            UsersInterfaceManager.Instance.SetActiveBoostceilBar(true);
            AudioSpawner.Instance.SpawnPlayerEffect(AudioSpawner.SoundEffectEnum.UpgradeItem);
            ItemDropSpawner.Instance.Despawn(transform.parent);
        }
    }
}