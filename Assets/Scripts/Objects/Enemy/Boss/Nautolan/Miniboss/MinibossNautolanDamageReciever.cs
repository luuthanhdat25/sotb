using Damage.RhythmScripts;
using DefaultNamespace;
using Enemy;
using UnityEngine;

public class MinibossNautolanDamageReciever : EnemyDamageReceiver
{
    protected override void OnDead()
    {
        base.OnDead();
        AudioManager.Instance.SpawnEnemyEffect(AudioManager.SoundEffectEnum.ExplosionBoss);
        MinibossNautolanCtrl.Instance.SetDeadAnimation();
        Debug.Log("BossDead");
        GameManager.Instance.IncreaseScore(scorePlus);
    }
}
