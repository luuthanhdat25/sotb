using System.Collections;
using Objects.Enemy.AttackEnemy;
using Player;
using UnityEngine;

namespace Enemy.TorpedoEnemy
{
    public class SpawnTorpedoEnemy : AbsEnemyShoot
    {
        [SerializeField] private float spawnYValue = 10f;
        [SerializeField] private float paddingHorizontal = 0.5f;
        
        protected override IEnumerator FireContinously()
        {
            Transform newProjectile = GetProjectile();
            if (newProjectile != null)
            {
                newProjectile.position = GetPositionSpawn();
                newProjectile.gameObject.SetActive(true);
            }
        
            yield return new WaitForSeconds(firingRate);
            firingRate -= Mathf.Abs(GetRandomPadding() / 10);
            firingCoroutine = null;
        }

        private Vector3 GetPositionSpawn()
            => new Vector3(PlayerPosition().x + GetRandomPadding(), spawnYValue);

        private float GetRandomPadding() => Random.Range(-paddingHorizontal, paddingHorizontal);
        
        private Vector3 PlayerPosition() => PlayerCtrl.Instance.GetCurrentPosition();
        
        protected override Transform GetProjectile()
            => EnemyProjectileSpawner.Instance.Spawn(EnemyProjectileSpawner.Instance.projectile5);
    }
}