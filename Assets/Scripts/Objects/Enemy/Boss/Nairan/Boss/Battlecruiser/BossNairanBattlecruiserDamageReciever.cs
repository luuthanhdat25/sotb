using DefaultNamespace;
using UnityEngine;

namespace Enemy.Boss.Nairan.Miniboss.Boss.Battlecruiser
{
    public class BossNairanBattlecruiserDamageReciever : EnemyDamageReceiver
    {
        protected override void OnDead()
        {
            base.OnDead();
            //BossNautolanCtrl.Instance.SetDeadAnimation();
            GameManager.Instance.IncreaseScore(scorePlus);
        }
    }
}