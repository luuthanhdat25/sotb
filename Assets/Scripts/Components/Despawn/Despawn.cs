using UnityEngine;

namespace Despawn
{
    public abstract class Despawn : RepeatMonoBehaviour
    {

        protected virtual void FixedUpdate()
        {
            this.Despawning();
        }
        
        protected virtual void Despawning()
        {
            if(!this.CanDespawn()) return;
            this.DespawnObject();
        }
        protected virtual void DespawnObject()
        {
            //For override
        }

        protected abstract bool CanDespawn();
    }
}