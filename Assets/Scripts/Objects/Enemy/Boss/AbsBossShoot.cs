using Comman;
using UnityEngine;

namespace Enemy.Boss
{
    public abstract class AbsBossShoot : AbsEnemyShoot
    {
        protected abstract override Transform GetProjectile();
        public float GetFiringRate() => this.firingRate;
        public virtual void SetIsFiring(bool isFiring) => this.isFiring = isFiring;
    }
}