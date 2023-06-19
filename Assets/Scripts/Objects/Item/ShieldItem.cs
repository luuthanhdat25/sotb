using Damage.RhythmScripts;
using Player;
using UnityEngine;

namespace Damage
{
    public class ShieldItem : Item
    {
        [SerializeField] private float timeEffective = 4f;
        public override void UseItem()
        {
            base.UseItem();
            PlayerCtrl.Instance.PlayerAnimations.SetActiveShield(true);
            PlayerCtrl.Instance.PlayerDamageReciever.ShieldUp(timeEffective);
            AudioManager.Instance.SpawnPlayerEffect(AudioManager.SoundEffectEnum.Buff);
            ItemDropSpawner.Instance.Despawn(transform.parent);
        }
    }
}