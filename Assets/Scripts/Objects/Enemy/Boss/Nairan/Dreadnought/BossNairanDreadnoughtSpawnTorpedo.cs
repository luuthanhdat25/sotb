using System;
using System.Collections;
using Enemy.Boss;
using Objects.Enemy.AttackEnemy;
using Player;
using UnityEngine;

namespace Objects.Enemy.Boss.Nairan.Dreadnought
{
    public class BossNairanDreadnoughtSpawnTorpedo : AbsBossShoot
    {
        [SerializeField] private float spawnYValue = 10f;

        private void Start() 
            => this.firingRate = BossNairanDreadnoughtCtrl.Instance.SpawnRate;

        protected override IEnumerator FireContinously()
        {
            Transform newProjectile = GetProjectile();
            if (newProjectile != null)
            {
                newProjectile.position = GetPositionSpawn();
                newProjectile.gameObject.SetActive(true);
            }
        
            yield return new WaitForSeconds(firingRate);
            firingCoroutine = null;
        }

        private Vector3 GetPositionSpawn()
            => new Vector3(PlayerPosition().x, spawnYValue, transform.parent.position.z);

        private Vector3 PlayerPosition() => PlayerCtrl.Instance.GetCurrentPosition();
        
        protected override Transform GetProjectile()
            => EnemyProjectileSpawner.Instance.Spawn(EnemyProjectileSpawner.Instance.projectile5);

        public override void SetIsFiring(bool isFiring)
        {
            if((!isFiring && this.isFiring) || (isFiring && !this.isFiring))
                this.isFiring = isFiring;
        }
    }
}