using UnityEngine;

namespace Damage
{
    public abstract class DamageSender : RepeatMonoBehaviour
    {
        [SerializeField] protected int damage = 1;

        public virtual int GetDamage()
        {
            return damage;
        }

        public abstract void GotHit();
    }
}