using System;
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

        public virtual void GotHit()
        {
        }
    }
}