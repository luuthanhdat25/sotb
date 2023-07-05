using Damage.RhythmScripts;
using DefaultNamespace;
using Enemy;
using Enemy.Boss.Nairan.Miniboss.Boss;
using Enemy.Boss.Nairan.Miniboss.Boss.Battlecruiser;
using UnityEngine;

namespace Objects.Enemy.Boss.Nairan.Dreadnought
{
    public class BossNairanDreadnoughtDamageReciever : EnemyDamageReceiver
    {
        protected override void OnDead()
        {
            base.OnDead();
            AudioSpawner.Instance.SpawnEnemyEffect(AudioSpawner.SoundEffectEnum.ExplosionBoss);
            BossNairanDreadnoughtCtrl.Instance.SetDeadAnimation();
            BossNairanDreadnoughtCtrl.Instance.IsDeadTrue();
            
            if(!DoubleBossNairanCtrl.Instance.Isdle2)
            {
                BossNairanBattlecruiserCtrl.Instance.BossNairanBattlecruiserDamageReciever.Reborn();
                AudioSpawner.Instance.SpawnPlayerEffect(AudioSpawner.SoundEffectEnum.Health);
            }
            DoubleBossNairanCtrl.Instance.OneShipDead();
            
            GameManager.Instance.IncreaseScore(scorePlus);
        }
    }
}