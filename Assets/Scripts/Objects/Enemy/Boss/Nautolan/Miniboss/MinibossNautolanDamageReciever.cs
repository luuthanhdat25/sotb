using Damage.RhythmScripts;
using DefaultNamespace;
using Enemy;
using UnityEngine;

public class MinibossNautolanDamageReciever : EnemyDamageReceiver
{
    protected override void OnDead()
    {
        base.OnDead();
        AudioSpawner.Instance.SpawnEnemyEffect(AudioSpawner.SoundEffectEnum.ExplosionBoss);
        MinibossNautolanCtrl.Instance.SetDeadAnimation();
        Debug.Log("BossDead");
        GameManager.Instance.IncreaseScore(scorePlus);
    }
}
