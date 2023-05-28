using Damage;
using UnityEngine;

public class PlayerProjectileDamageReceiver : DamageReceiver
{
    protected override void OnDead()
    {
        PlayerProjectileSpawner.Instance.Despawn(transform.parent);
    }
}
