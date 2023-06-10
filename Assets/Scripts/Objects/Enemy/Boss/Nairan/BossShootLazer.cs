using System.Collections;
using Objects.Enemy.AttackEnemy;
using UnityEngine;

namespace Enemy.Boss.Nairan.Miniboss
{
    public class BossShootLazer : MonoBehaviour
    {
        [SerializeField] protected bool isTemporaryLazer;
        protected Transform lazerTransform = null; 
        protected bool isLazerOn = false;
        
        public virtual void SpawnLazerAfterTime(float time)
        {
            if (isLazerOn) return;
            isLazerOn = true;
            StartCoroutine(SpawnLazer(time));
        }

        private IEnumerator SpawnLazer(float time)
        {
            yield return new WaitForSeconds(time);
            lazerTransform = GetProjectile();
            if (lazerTransform != null)
            {
                lazerTransform.position = transform.parent.position;
                lazerTransform.parent = transform.parent;
                lazerTransform.gameObject.SetActive(true);
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