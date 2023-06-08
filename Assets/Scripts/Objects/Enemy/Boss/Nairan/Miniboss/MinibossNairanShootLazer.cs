using System.Collections;
using Comman;
using Objects.Enemy.AttackEnemy;
using UnityEngine;

namespace Enemy.Boss.Nairan.Miniboss
{
    public class MinibossNairanShootLazer : MonoBehaviour
    {
        private Transform lazerTransform;
        public bool isLazerOn = false;
        public void SpawnLazer()
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
        protected virtual Transform GetProjectile() => EnemyProjectileSpawner.Instance.Spawn(EnemyProjectileSpawner.Instance.projectile1_Miniboss_Kla_ed1);
        
        public void DespawnLazer()
        {
            if (!lazerTransform) return;
            EnemyProjectileSpawner.Instance.PushToHolderManager(lazerTransform);
            EnemyProjectileSpawner.Instance.Despawn(lazerTransform);
            isLazerOn = false;
        }
    }
}