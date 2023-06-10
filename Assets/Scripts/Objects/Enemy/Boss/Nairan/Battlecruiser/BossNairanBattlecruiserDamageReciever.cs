using DefaultNamespace;
using UnityEngine;

namespace Enemy.Boss.Nairan.Miniboss.Boss.Battlecruiser
{
    public class BossNairanBattlecruiserDamageReciever : EnemyDamageReceiver
    {
        protected override void OnDead()
        {
            base.OnDead();
            BossNairanBattlecruiserCtrl.Instance.SetDeadAnimation();
            BossNairanBattlecruiserCtrl.Instance.IsDeadTrue();
            DoubleBossNairanCtrl.Instance.OneShipDead();
            GameManager.Instance.IncreaseScore(scorePlus);
        }
    }
}