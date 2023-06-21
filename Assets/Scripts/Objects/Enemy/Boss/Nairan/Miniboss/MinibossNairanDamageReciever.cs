using Damage.RhythmScripts;
using DefaultNamespace;
using UnityEngine;

namespace Enemy.Boss.Nairan.Miniboss
{
    public class MinibossNairanDamageReciever : EnemyDamageReceiver
    {
        protected override void OnDead()
        {
            base.OnDead();
            AudioSpawner.Instance.SpawnEnemyEffect(AudioSpawner.SoundEffectEnum.ExplosionBoss);
            MiniBossNairanCtrl.Instance.SetDeadAnimation();
            Debug.Log("BossDead");
            GameManager.Instance.IncreaseScore(scorePlus);
        }
    }
}