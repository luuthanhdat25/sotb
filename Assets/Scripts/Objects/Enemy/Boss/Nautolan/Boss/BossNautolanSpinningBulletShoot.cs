using System.Collections;
using Objects.Enemy.AttackEnemy;
using UnityEngine;

public class BossNautolanSpinningBulletShoot : MinibossNautolanShoot
{
    protected override IEnumerator FireContinously()
    {
        BossNautolanCtrl.Instance.BossNautolanModelShipAnimation.SetIsFollowAndShoot(true);
        Transform newProjectile = GetProjectile();
        if (newProjectile != null)
        {
            newProjectile.position = transform.parent.position;
            newProjectile.gameObject.SetActive(true);
        }
        
        yield return new WaitForSeconds(firingRate);
        BossNautolanCtrl.Instance.BossNautolanModelShipAnimation.SetIsFollowAndShoot(false);
        firingCoroutine = null;
    }
    protected override Transform GetProjectile() => EnemyProjectileSpawner.Instance.Spawn(EnemyProjectileSpawner.Instance.projectile2);
}
