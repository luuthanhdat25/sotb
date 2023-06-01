using UnityEngine;

namespace Despawn
{
    public abstract class Despawn : RepeatMonoBehaviour
    {
        protected virtual void FixedUpdate() =>this.Despawning();

        protected virtual void Despawning()
        {
            if(!this.CanDespawn()) return;
            this.DespawnObject();
        }

        protected abstract bool CanDespawn();
        protected abstract void DespawnObject();
    }
}