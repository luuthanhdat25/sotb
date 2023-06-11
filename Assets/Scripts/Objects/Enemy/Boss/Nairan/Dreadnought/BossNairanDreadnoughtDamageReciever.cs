using DefaultNamespace;
using Enemy;
using Enemy.Boss.Nairan.Miniboss.Boss;
using UnityEngine;

namespace Objects.Enemy.Boss.Nairan.Dreadnought
{
    public class BossNairanDreadnoughtDamageReciever : EnemyDamageReceiver
    {
        protected override void OnDead()
        {
            base.OnDead();
            BossNairanDreadnoughtCtrl.Instance.SetDeadAnimation();
            BossNairanDreadnoughtCtrl.Instance.IsDeadTrue();
            DoubleBossNairanCtrl.Instance.OneShipDead();
            GameManager.Instance.IncreaseScore(scorePlus);
        }
    }
}