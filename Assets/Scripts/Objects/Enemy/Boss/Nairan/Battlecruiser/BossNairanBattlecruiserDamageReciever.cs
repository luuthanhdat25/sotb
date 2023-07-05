using Damage.RhythmScripts;
using DefaultNamespace;
using Objects.Enemy.Boss.Nairan.Dreadnought;
using UnityEngine;

namespace Enemy.Boss.Nairan.Miniboss.Boss.Battlecruiser
{
    public class BossNairanBattlecruiserDamageReciever : EnemyDamageReceiver
    {
        protected override void OnDead()
        {
            base.OnDead();
            AudioSpawner.Instance.SpawnEnemyEffect(AudioSpawner.SoundEffectEnum.ExplosionBoss);
            BossNairanBattlecruiserCtrl.Instance.SetDeadAnimation();
            BossNairanBattlecruiserCtrl.Instance.IsDeadTrue();
            if (!DoubleBossNairanCtrl.Instance.Isdle2)
            {
                BossNairanDreadnoughtCtrl.Instance.BossNairanDreadnoughtDamageReciever.Reborn();
                AudioSpawner.Instance.SpawnPlayerEffect(AudioSpawner.SoundEffectEnum.Health);
            }
            DoubleBossNairanCtrl.Instance.OneShipDead();
            GameManager.Instance.IncreaseScore(scorePlus);
        }
    }
}