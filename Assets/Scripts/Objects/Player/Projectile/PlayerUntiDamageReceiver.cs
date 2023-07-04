using UnityEngine;

namespace Projectile
{
    public class PlayerUntiDamageReceiver : PlayerProjectileDamageReceiver
    {
        [SerializeField] private ParticleSystem collisionEffect;
        [SerializeField] private ParticleSystem explosionEffect;
        
        protected override void OnDead()
        {
            this.CreateImpactFX(explosionEffect);
            PlayerProjectileSpawner.Instance.Despawn(transform.parent);
        }
        
        protected void CreateImpactFX(ParticleSystem particle)
        {
            if(particle == null) return;
            ParticleSystem instance = Instantiate(particle);
            Vector3 directionSpawn = Vector3.up;
            instance.transform.LookAt(directionSpawn);
            instance.transform.position = transform.parent.position + fxOffSet;
            Destroy(instance.gameObject, instance.main.duration);
        }
        
        public override void Deduct(int hpDeduct)
        {
            base.Deduct(hpDeduct);
            CreateImpactFX(collisionEffect);
        }
    }
}