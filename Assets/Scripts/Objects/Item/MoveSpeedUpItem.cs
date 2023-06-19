using Damage.RhythmScripts;
using Player;
using UnityEngine;

namespace Damage
{
    public class MoveSpeedUpItem : Item
    {
        [SerializeField] private float addValue = 1.5f;
        [SerializeField] private float timeEffective = 2f;
        public override void UseItem()
        {
            base.UseItem();
            PlayerCtrl.Instance.PlayerMovement.AddMoveSpeedInTime(this.addValue, this.timeEffective);
            PlayerCtrl.Instance.PlayerBootCeils.AddBoostCeils(2);
            PlayerCtrl.Instance.PlayerParticleEffect.BuffMoveSpeedEffect();
            AudioManager.Instance.SpawnPlayerEffect(AudioManager.SoundEffectEnum.Buff);
            ItemDropSpawner.Instance.Despawn(transform.parent);
        }
    }
}