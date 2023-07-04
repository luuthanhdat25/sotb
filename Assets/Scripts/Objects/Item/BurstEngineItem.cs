using System;
using Damage.RhythmScripts;
using Objects.UI.HUD;
using Player;
using UnityEngine;

namespace Damage
{
    public class BurstEngineItem : Item
    {
        [SerializeField] private GameObject tutorialUI;
        public override void UseItem()
        {
            base.UseItem();
            PlayerCtrl.Instance.PlayerAnimations.SetBurstEngineUpgrade();
            PlayerCtrl.Instance.PlayerMovement.SetCanMoveNormal(true);
            PlayerCtrl.Instance.PlayerMovement.SetCanUseDashToTrue();
            
            UIManager.Instance.SetActiveBoostceilBar(true);
            Invoke("SetActiveTutorial", 1f);
            AudioSpawner.Instance.SpawnPlayerEffect(AudioSpawner.SoundEffectEnum.UpgradeItem);
            
            ItemDropSpawner.Instance.Despawn(transform.parent);
        }
        
        private void SetActiveTutorial() => tutorialUI?.SetActive(true);
    }
}