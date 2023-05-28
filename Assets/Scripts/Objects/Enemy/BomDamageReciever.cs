using Damage;
using Objects.Enemy.AttackEnemy;
using UnityEngine;

public class BomDamageSender : DamageReceiver
{
    protected override void OnDead()
    {
        this.OnDeadFX();
        EnemyProjectileSpawner.Instance.Despawn(transform.parent);
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
