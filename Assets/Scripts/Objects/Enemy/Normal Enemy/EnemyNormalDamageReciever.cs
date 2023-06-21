using Damage.RhythmScripts;
using DefaultNamespace;
using UnityEngine;

namespace Enemy
{
    public class EnemyNormalDamageReciever : EnemyDamageReceiver
    {
        [SerializeField] private DropBom dropBom = null;
        protected override void OnDead()
        {
            base.OnDead();
            this.OnDeadFX();
            AudioSpawner.Instance.SpawnEnemyEffect(AudioSpawner.SoundEffectEnum.ExplosionNormalEnemy);
            if(dropBom != null) dropBom.Drop();
            GameManager.Instance.IncreaseScore(scorePlus);
            transform.parent.gameObject.SetActive(false);
        }

        protected virtual void OnDeadFX()
        {
            string fxName = this.GetRandomFXName();
            Transform fxOnDead = FXSpawner.Instance.Spawn(fxName);
            fxOnDead.position = transform.parent.position;
            fxOnDead.gameObject.SetActive(true);
        }

        protected virtual string GetRandomFXName()
        {
            return "Smoke_" + UnityEngine.Random.Range(1, 4);
        }
    }
}