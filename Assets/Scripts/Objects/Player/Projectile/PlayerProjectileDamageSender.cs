using Damage;
using UnityEngine;

namespace Projectile
{
    public class PlayerProjectileDamageSender : DamageSender
    {
        private Vector3 fxOffSet = new Vector3(0, 0.4f, 0);
        public override void GotHit()
        {
            //PlayerProjectileSpawner.Instance.Despawn(transform.parent);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            this.CreateImpactFX(col);
        }

        private void CreateImpactFX(Collider2D other)
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
}