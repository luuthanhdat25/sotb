using Damage;
using Damage.RhythmScripts;
using DefaultNamespace;
using UnityEngine;

namespace Enemy
{
    public abstract class EnemyDamageReceiver: DamageReceiver
    {
        [SerializeField] protected EnemySO enemySO;
        [SerializeField] protected int scorePlus = 100;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadEnemySO();
        }

        protected virtual void LoadEnemySO()
        {
            if(this.enemySO != null) return;
            string resPath = "EnemyInfor/" + transform.parent.name;
            this.enemySO = Resources.Load<EnemySO>(resPath);
            Debug.LogWarning(transform.name + ": Load EnemySO" + resPath, gameObject);
        }
        
        public override void Reborn()
        {
            this.hpMax = this.enemySO.hpMax;
            base.Reborn();
        }

        protected override void OnDead() => this.DropItem();

        protected virtual void DropItem()
        {
            if (this.enemySO.dropList.Count == 0) return;
            ItemDropSpawner.Instance.Drop(this.enemySO.dropList, transform.parent.position);
        }
        
        public override void Deduct(int hpDeduct)
        {
            base.Deduct(hpDeduct);
            PlaySFX();
        }

        private void PlaySFX() => AudioSpawner.Instance.SpawnEnemyEffect(AudioSpawner.SoundEffectEnum.Hurt);
        
        public int HpCurrent => hpCurrent;
        public int HpMax => hpMax;
    }
}