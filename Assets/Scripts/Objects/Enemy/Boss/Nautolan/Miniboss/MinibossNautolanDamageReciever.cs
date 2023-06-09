using DefaultNamespace;
using Enemy;
using UnityEngine;

public class MinibossNautolanDamageReciever : EnemyDamageReceiver
{
    protected override void OnDead()
    {
        base.OnDead();
        MinibossNautolanCtrl.Instance.SetDeadAnimation();
        Debug.Log("BossDead");
        GameManager.Instance.IncreaseScore(scorePlus);
    }
}
