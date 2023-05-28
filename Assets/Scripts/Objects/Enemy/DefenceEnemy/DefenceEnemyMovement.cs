using Others;
using UnityEngine;

public class DefenceEnemyMovement : ObjectMovement
{
    private Pathfinder pathfinder;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPathfinder();
    }

    private void LoadPathfinder()
    {
        if (this.pathfinder != null) return;
        this.pathfinder = transform.parent.GetComponent<Pathfinder>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.ChangeDirection();
    }
    
    
    protected override void ChangeDirection()
    {
        /*Vector3 vectorRotation = this.pathfinder.GetNextPointPosition() - transform.parent.position;
        vectorRotation.Normalize();
        float rotationZ = Mathf.Atan2(vectorRotation.x, vectorRotation.y) * Mathf.Rad2Deg;
        transform.parent.rotation = Quaternion.Euler(0f, 0f, rotationZ);*/
    }
    
    protected override void ChangeSpeed()
    {
        abstractMoveSpeed = 0;
    }

}
