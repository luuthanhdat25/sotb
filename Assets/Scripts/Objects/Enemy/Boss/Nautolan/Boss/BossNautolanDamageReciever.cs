using Damage.RhythmScripts;
using DefaultNamespace;
using UnityEngine;

namespace Enemy.Nautolan
{
    public class BossNautolanDamageReciever : EnemyDamageReceiver
    {
        protected override void OnDead()
        {
            base.OnDead();
            AudioManager.Instance.SpawnEnemyEffect(AudioManager.SoundEffectEnum.ExplosionBoss);
            BossNautolanCtrl.Instance.SetDeadAnimation();
            Debug.Log("BossDead");
            GameManager.Instance.IncreaseScore(scorePlus);
            GameManager.Instance.WinGame();
        }

        public bool IsLowerHealth(float persent)
        {
            byte currentPersent = (byte)(((float)hpCurrent / (float)hpMax) * 100);
            Debug.Log(currentPersent);
            return currentPersent < persent;
        }
    }
}