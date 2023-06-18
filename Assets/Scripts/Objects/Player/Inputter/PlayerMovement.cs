using System.Collections;
using DefaultNamespace;
using Player;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Speed 
    [SerializeField] private float totalSpeed;
    [SerializeField] private float basicMoveSpeed = 5f;
    [SerializeField] private bool canMoveNormal = false;
    [SerializeField] private bool canUseDash = false;
    [SerializeField] private float dashSpeed = 7f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private int dashEnergiesUse = 1;
    private bool alreadyDash = false;
    
    [Header("Paddings")] 
    private Vector2 minBounds;
    private Vector2 maxBounds;
    [SerializeField] private float paddingLeft;
    [SerializeField] private float paddingRight;
    [SerializeField] private float paddingTop;
    [SerializeField] private float paddingBottom;

    private void Start()
    {
        InitializeBounds();
    }
    
    private void InitializeBounds()
    {
        //Use var to easyer to change
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }
    //---------------------------------------------------------------------------//
    private void FixedUpdate()
    {
        HandleMovement();
    }
    
    private void HandleMovement()
    {
        if (PlayerCtrl.Instance.GetIsPlayerDead()) return;
        if (IsDash())
        {
            PlayerCtrl.Instance.PlayerBootCeils.DeductCeilsByValue(dashEnergiesUse);
            StartCoroutine(Dash());
        }
        else 
            MoveFollowNormalSpeed();
    }
    
    public bool IsDash()
    {
        if (!canUseDash) return false;
        if (GameInput.Instance.IsDashPressed() && alreadyDash) return false;
        if (GameInput.Instance.IsDashPressed() && GameInput.Instance.GetRawInputNormalized() != Vector2.zero && PlayerCtrl.Instance.PlayerBootCeils.CanDeductByValue(dashEnergiesUse))
        {
            alreadyDash = true;
            return true;
        }
        else
        {
            alreadyDash = false;
            return false;
        }
    }

    private IEnumerator Dash()
    {
        canMoveNormal = false;
        totalSpeed = dashSpeed;
        PlayerCtrl.Instance.PlayerDamageReciever.SetActiveCollider(false);
        PlayerCtrl.Instance.PlayerParticleEffect.DashEffect(dashTime);
        PlayerCtrl.Instance.PlayerAnimations.SpriteBlur();
        float startTime = Time.time;
        float endTime = startTime + dashTime;
        while (Time.time < endTime)
        {
            this.MoveBySpeedAndInputPlayer(totalSpeed);
            yield return null;
        }
        PlayerCtrl.Instance.PlayerDamageReciever.SetActiveCollider(true);
        PlayerCtrl.Instance.PlayerAnimations.ResetOpacity();
        canMoveNormal = true;
    }
    
    
    private void MoveBySpeedAndInputPlayer(float speed)
    {
        var velocity = CalculateVelocity(speed);
        var newPosition = CalculateNewPosition(velocity);
        this.MoveToNewPosition(newPosition);
    }
    
    private Vector3 CalculateVelocity(float speed)
    {
        Vector3 moveDirection = GameInput.Instance.GetRawInputNormalized();
        float realSpeed = speed * Time.fixedDeltaTime;
        return moveDirection * realSpeed;
    }
    
    private Vector2 CalculateNewPosition(Vector3 velocity)
    {
        Vector2 newPosition = new Vector2();
        //Clamps the given value between the given minimum float and maximum float values
        newPosition.x = Mathf.Clamp(transform.parent.position.x + velocity.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPosition.y = Mathf.Clamp(transform.parent.position.y + velocity.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        return newPosition;
    }
    
    private void MoveToNewPosition(Vector2 newPosition) => transform.parent.position = newPosition;
    
    private void MoveFollowNormalSpeed()
    {
        if (!canMoveNormal) return;
        totalSpeed = basicMoveSpeed;
        var velocity = CalculateVelocity(totalSpeed);
        var newPosition = CalculateNewPosition(velocity);
        this.MoveToNewPosition(newPosition);
    }
    
    public float GetTotalSpeed() => this.totalSpeed;
    public void SetCanUseDashToTrue() => this.canUseDash = true;
    public void SetCanMoveNormal(bool canMove) => this.canMoveNormal = canMove;
    public void AddMoveSpeedInTime(float value, float time) => StartCoroutine(CouroutineAddMoveSpeed(value, time));

    IEnumerator CouroutineAddMoveSpeed(float value, float time)
    {
        this.basicMoveSpeed += value;
        yield return new WaitForSeconds(time);
        this.basicMoveSpeed -= value + value / 10;
    }
}
