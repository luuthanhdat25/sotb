using Damage;
using UnityEngine;

public class PlayerProjectileDamageReceiver : DamageReceiver
{
    protected Vector3 fxOffSet = new Vector3(0, 0.4f, 0);
    protected override void OnDead()
    {
        this.CreateImpactFX();
        PlayerProjectileSpawner.Instance.Despawn(transform.parent);
    }
    
    protected virtual void CreateImpactFX()
    {
        string fxName = this.GetRandomImpactFXName();
        Transform fxImpact = FXSpawner.Instance.Spawn(fxName);
        fxImpact.position = transform.parent.position + fxOffSet;
        fxImpact.rotation = transform.parent.rotation;
        fxImpact.gameObject.SetActive(true);
    }
    
    protected virtual string GetRandomImpactFXName()
    {
        return "Impact_" + UnityEngine.Random.Range(1, 4);
    }
}
