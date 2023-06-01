using UnityEngine;

namespace Damage
{
    public abstract class DamageReceiver : RepeatMonoBehaviour
    {
        [Header("DamageReceiver")] 
        [SerializeField] protected int hpCurrent = 1;
        [SerializeField] protected int hpMax = 5;
        [SerializeField] protected bool isDead = false;

        private void OnEnable() => Reborn();

        public virtual void Reborn()
        {
            this.hpCurrent = this.hpMax;
            this.isDead = false;
        }

        public virtual void Add(int hpAdd)
        {
            if (this.isDead) return;
            if (this.hpCurrent >= this.hpMax) return;
            
            this.hpCurrent += hpAdd;
        }

        public virtual void Deduct(int hpDeduct)
        {
            if (IsDead()) return;
            
            this.hpCurrent -= hpDeduct;
            this.CheckIsDead();
        }

        protected virtual bool IsDead() => this.hpCurrent <= 0;
        
        protected virtual void CheckIsDead()
        {
            if (!this.IsDead()) return;
            this.isDead = true;
            this.OnDead();
        }

        protected abstract void OnDead();

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent<DamageSender>(out DamageSender damageSender))
            {
                this.Deduct(damageSender.GetDamage());
                damageSender.GotHit();
            }
        }

        public int GetCurrentHp() => this.hpCurrent;
        public int GetHpMax() => this.hpMax;
    }
}