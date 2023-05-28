using DefaultNamespace;
using Enemy;
using UnityEngine;

public class MinibossNautolanDamageReciever : EnemyDamageReceiver
{
    [SerializeField] private int scorePlus = 1000;

    protected override void OnDead()
    {
        base.OnDead();
        MinibossNautolanCtrl.Instance.SetDeadAnimation();
        Debug.Log("BossDead");
        GameManager.Instance.IncreaseScore(scorePlus);
        GameManager.Instance.WinGame();
    }
}
