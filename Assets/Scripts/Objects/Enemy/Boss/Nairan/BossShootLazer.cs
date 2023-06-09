using System.Collections;
using Comman;
using Objects.Enemy.AttackEnemy;
using UnityEngine;

namespace Enemy.Boss.Nairan.Miniboss
{
    public class BossShootLazer : MonoBehaviour
    {
        [SerializeField] protected bool isTemporaryLazer;
        protected Transform lazerTransform; 
        protected bool isLazerOn = false;
        
        public virtual void SpawnLazer()
        {
            if (isLazerOn) return;
            lazerTransform = GetProjectile();
            if (lazerTransform != null)
            {
                lazerTransform.position = transform.parent.position;
                lazerTransform.parent = transform.parent;
                lazerTransform.gameObject.SetActive(true);
                isLazerOn = true;
            }
        }
        protected virtual Transform GetProjectile()
        {
            if(isTemporaryLazer) 
                return EnemyProjectileSpawner.Instance.Spawn(EnemyProjectileSpawner.Instance.projectile2);
            return EnemyProjectileSpawner.Instance.Spawn(EnemyProjectileSpawner.Instance.projectile3);
        }
        
        public virtual void DespawnLazer()
        {
            if (!lazerTransform) return;
            EnemyProjectileSpawner.Instance.PushToHolderManager(lazerTransform);
            EnemyProjectileSpawner.Instance.Despawn(lazerTransform);
            lazerTransform = null;
            isLazerOn = false;
        }

        public void SetIsTemporaryLazer(bool isTemporary) => this.isTemporaryLazer = isTemporary;
    }
}