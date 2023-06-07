using System.Collections;
using Comman;
using Objects.Enemy.AttackEnemy;
using UnityEngine;

namespace Enemy.Boss.Nairan.Miniboss
{
    public class MinibossNairanShootLazer : MonoBehaviour
    {
        private Transform lazerTransform;
        public bool isOn = false;
        public void SpawnLazer()
        {
            lazerTransform = GetProjectile();
            if (lazerTransform != null)
            {
                lazerTransform.position = transform.parent.position;
                lazerTransform.gameObject.SetActive(true);
            }
        }
        
        public void DespawnLazer()
        {
            EnemyProjectileSpawner.Instance.Despawn(lazerTransform);
        }
        
        protected virtual Transform GetProjectile() => EnemyProjectileSpawner.Instance.Spawn(EnemyProjectileSpawner.Instance.projectile1_Miniboss_Kla_ed1);
    }
}