using DefaultNamespace;
using UnityEngine;

namespace Enemy.Nautolan
{
    public class BossNautolanDamageReciever : EnemyDamageReceiver
    {
        protected override void OnDead()
        {
            base.OnDead();
            BossNautolanCtrl.Instance.SetDeadAnimation();
            Debug.Log("BossDead");
            GameManager.Instance.IncreaseScore(scorePlus);
            GameManager.Instance.WinGame();
        }

        public bool IsLowerHealth(float persent)
        {
            int currentPersent = (hpCurrent / hpMax) * 100;
            return currentPersent > persent;
        }
    }
}