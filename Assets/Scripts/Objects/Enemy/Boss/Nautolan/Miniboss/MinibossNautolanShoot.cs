using System.Collections;
using Comman;
using Objects.Enemy.AttackEnemy;
using UnityEngine;

public class MinibossNautolanShoot : Shoot
{
    protected override void Fire()
    {
        if (isFiring && firingCoroutine == null)
            firingCoroutine = StartCoroutine(FireContinously());
    }
        
    private IEnumerator FireContinously()
    {
        Transform newProjectile = GetProjectile();
        if (newProjectile != null)
        {
            newProjectile.position = transform.parent.position;
            newProjectile.gameObject.SetActive(true);
        }
        
        yield return new WaitForSeconds(firingRate);
        firingCoroutine = null;
    }
    
    protected virtual Transform GetProjectile() => EnemyProjectileSpawner.Instance.Spawn(EnemyProjectileSpawner.Instance.projectile1);
    
    public float GetFiringRate() => this.firingRate;
    public void SetIsFiring(bool isFiring) => this.isFiring = isFiring;
    public void DoubleFiringRate() => this.firingRate /= 2;
}
