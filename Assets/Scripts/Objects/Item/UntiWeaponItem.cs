using Damage.RhythmScripts;
using Player;
using UnityEngine;

namespace Damage
{
    public class UntiWeaponItem : Item
    {
        [SerializeField] private GameObject tutorialUI;
        
        public override void UseItem()
        {
            base.UseItem();
            PlayerCtrl.Instance.PlayerAnimations.TurnOnUntiWeapon();
            PlayerCtrl.Instance.PlayerUnti.SetIsHasUntiWeaponTrue();
            
            AudioSpawner.Instance.SpawnPlayerEffect(AudioSpawner.SoundEffectEnum.UpgradeItem);
            SetActiveTutorial();
            ItemDropSpawner.Instance.Despawn(transform.parent);
        }
        
        private void SetActiveTutorial()
        {
            tutorialUI?.SetActive(true);
            AudioSpawner.Instance.UIEffect();
        }
    }
}