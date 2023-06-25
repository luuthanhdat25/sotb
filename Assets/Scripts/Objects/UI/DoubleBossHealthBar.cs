using Damage;
using UnityEngine;

namespace DefaultNamespace.Objects.UI
{
    public class DoubleBossHealthBar : BossHealthBar
    {
        [SerializeField] private DamageReceiver damageReceiver2;
        
        protected override void Start()
        {
            maxHealth = damageReceiver.GetHpMax() + damageReceiver2.GetHpMax();
            SetMaxHealth(maxHealth);
        }

        protected override int GetCurrentHealth() => damageReceiver.GetCurrentHp() + damageReceiver2.GetCurrentHp();
    }
}