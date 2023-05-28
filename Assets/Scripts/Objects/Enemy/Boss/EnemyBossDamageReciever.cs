using DefaultNamespace;
using UnityEngine;

namespace Enemy
{
    public class EnemyBossDamageReciever : EnemyDamageReceiver
    {
        [SerializeField] private int scorePlus = 1000;

        protected override void OnDead()
        {
            base.OnDead();
            MiniBossKla_edCtrl.Instance.SetDeadAnimation();
            Debug.Log("BossDead");
            GameManager.Instance.IncreaseScore(scorePlus);
            GameManager.Instance.WinGame();
        }
    }
}