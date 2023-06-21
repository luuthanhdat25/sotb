using Damage.RhythmScripts;
using DefaultNamespace;
using UnityEngine;

namespace Enemy
{
    public class MinibossKla_edDamageReciever : EnemyDamageReceiver
    {
        protected override void OnDead()
        {
            base.OnDead();
            AudioManager.Instance.SpawnEnemyEffect(AudioManager.SoundEffectEnum.ExplosionBoss);
            MiniBossKla_edCtrl.Instance.SetDeadAnimation();
            Debug.Log("BossDead");
            GameManager.Instance.IncreaseScore(scorePlus);
            GameManager.Instance.WinGame();
        }
    }
}