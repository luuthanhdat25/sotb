using Damage.RhythmScripts;
using DefaultNamespace;
using UnityEngine;

namespace Enemy.Nautolan
{
    public class BossNautolanDamageReciever : EnemyDamageReceiver
    {
        [SerializeField] private Transform shootSpeedItem;
        [SerializeField] private Transform moveSpeedItem;
        [SerializeField] private Transform energyItem;
        
        protected override void OnDead()
        {
            base.OnDead();
            AudioSpawner.Instance.SpawnEnemyEffect(AudioSpawner.SoundEffectEnum.ExplosionBoss);
            BossNautolanCtrl.Instance.SetDeadAnimation();
            Debug.Log("BossDead");
            GameManager.Instance.IncreaseScore(scorePlus);
            GameManager.Instance.WinGame();
        }

        public bool IsLowerHealth(float persent)
        {
            float currentPersent = (((float)hpCurrent / (float)hpMax) * 100);
            return currentPersent < persent;
        }
        
        public override void Deduct(int hpDeduct)
        {
            base.Deduct(hpDeduct);
            if (hpCurrent == 84 && moveSpeedItem != null)
                ItemDropSpawner.Instance.Drop(moveSpeedItem, transform.parent.position);
            
            if (hpCurrent == 50 && energyItem != null)
                ItemDropSpawner.Instance.Drop(energyItem, transform.parent.position);
            
            if(hpCurrent == 30 && shootSpeedItem != null) 
                ItemDropSpawner.Instance.Drop(shootSpeedItem, transform.parent.position);
        }
    }
}