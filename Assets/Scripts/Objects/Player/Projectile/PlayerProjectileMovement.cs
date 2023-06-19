using DefaultNamespace;
using Others;
using Player;
using UnityEngine;

public class PlayerProjectileMovement : ObjectMovement
{
    [SerializeField] protected float moveSpeed;
    
    [Range(0, 1)]
    [SerializeField] protected float deviation;
    private const int PERSENT_DEVIATION = 100;
    
    protected override void ChangeSpeed()
    {
        abstractMoveSpeed = this.moveSpeed;
    }

    protected override void ChangeDirection()
    {
        abstractDirection = CaculateBasePlayerSpeed();
        //transform.parent.Rotate(abstractDirection);
    }

    private Vector2 CaculateBasePlayerSpeed()
    {
        if (GameInput.Instance.GetRawInputNormalized() == Vector2.zero)
        {
            return Vector2.up;
        }
        else
        {
            deviation = PlayerCtrl.Instance.PlayerMovement.BasicMoveSpeed / PERSENT_DEVIATION;
            return Vector2.up + GameInput.Instance.GetRawInputNormalized() * deviation;
        }
    }
}
