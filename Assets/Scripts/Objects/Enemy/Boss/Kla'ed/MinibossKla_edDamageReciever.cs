using Damage.RhythmScripts;
using DefaultNamespace;
using UnityEngine;

namespace Enemy
{
    public class MinibossKla_edDamageReciever : EnemyDamageReceiver
    {
        [SerializeField] private Transform shootSpeedItem;
        [SerializeField] private Transform energyItem;
        
        protected override void OnDead()
        {
            base.OnDead();
            AudioSpawner.Instance.SpawnEnemyEffect(AudioSpawner.SoundEffectEnum.ExplosionBoss);
            MiniBossKla_edCtrl.Instance.SetDeadAnimation();
            Debug.Log("BossDead");
            GameManager.Instance.IncreaseScore(scorePlus);
            GameManager.Instance.WinGame();
        }
        
        public override void Deduct(int hpDeduct)
        {
            base.Deduct(hpDeduct);
            if (hpCurrent == 60 && shootSpeedItem != null)
                ItemDropSpawner.Instance.Drop(shootSpeedItem, transform.parent.position);
            
            if(hpCurrent == 30 && energyItem != null) 
                ItemDropSpawner.Instance.Drop(energyItem, transform.parent.position);
        }
    }
}