using DefaultNamespace;
using UnityEngine;

namespace Enemy.Boss.Nairan.Miniboss
{
    public class MinibossNairanDamageReciever : EnemyDamageReceiver
    {
        [SerializeField] private int scorePlus = 1000;

        protected override void OnDead()
        {
            base.OnDead();
            //MinibossNautolanCtrl.Instance.SetDeadAnimation();
            Debug.Log("BossDead");
            GameManager.Instance.IncreaseScore(scorePlus);
        }
    }
}