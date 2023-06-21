using Damage.RhythmScripts;
using Player;
using UnityEngine;

namespace Damage
{
    public class ShootSpeedUpItem : Item
    {
        [SerializeField] private float reductionRate = 1.2f;
        [SerializeField] private float timeEffective = 2f;
        public override void UseItem()
        {
            base.UseItem();
            PlayerCtrl.Instance.PlayerShoot.DecreaseFireRateInTime(this.reductionRate, this.timeEffective);
            AudioSpawner.Instance.SpawnPlayerEffect(AudioSpawner.SoundEffectEnum.Buff);
            PlayerCtrl.Instance.PlayerParticleEffect.BuffShootSpeedEffect();
            ItemDropSpawner.Instance.Despawn(transform.parent);
        }
    }
}