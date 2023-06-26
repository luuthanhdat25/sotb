using System.Collections;
using System.Collections.Generic;
using Enemy.Boss.Nairan.Miniboss.Boss.Battlecruiser;
using UnityEngine;

public class BossNairanBattlecruiserArcShockWaveSMB : StateMachineBehaviour
{
    [SerializeField] protected float curveHeight = 2f;
    [SerializeField] protected float firingRate = 0.7f;
    [SerializeField] protected float speedArc;
    [SerializeField] protected float speedToStartPosition;
    private int numberLoop = 2;
    private float timer = 0f;       
    private Vector3 centerPoint;    
    private Vector3 startPosition;    
    private Vector3 endPosition; 
    private bool moveToStartPoint = true;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BossNairanBattlecruiserCtrl.Instance.BossNairanBattlecruiserShootShockWave.SetFiringRate(firingRate);
        startPosition = GetStartPosition();
        endPosition = GetEndPosition();
        numberLoop = GetNumberLoop();
        speedArc = GetSpeedArc();
        speedToStartPosition = GetSpeedToStartPosition();
    }
    
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SetAnimaiton(true);
        if (moveToStartPoint)
            MoveToStartPoint(animator);
        else
            MoveAlongArc(animator);
    }

    protected virtual void MoveToStartPoint(Animator animator)
    {
        if (animator.transform.position != startPosition)
            animator.transform.position = Vector3.MoveTowards(animator.transform.position, startPosition, speedToStartPosition * Time.deltaTime);
        else
            moveToStartPoint = false;  
    }

    protected virtual void MoveAlongArc(Animator animator)
    {
        if(numberLoop >= 0)
        {
            if (timer < 1f)
            {
                SetProjectile(true);
                Vector3 currentPos = Vector3.Lerp(startPosition, endPosition, timer);
                currentPos.y -= Mathf.Sin(timer * Mathf.PI) * curveHeight;
                animator.transform.position = currentPos;
                timer += Time.deltaTime * speedArc;
            }
            else
            {
                animator.transform.position = endPosition;
                timer = 0;
                SwapStartAndEndPosition();
                numberLoop--;
            }
        }
        else
        {
            moveToStartPoint = true;
            SetProjectile(false);
            SetAnimaiton(false);
            UnSetAniamtion();
        }
    }

    private void SwapStartAndEndPosition()
        => (startPosition, endPosition) = (endPosition, startPosition);
    

    protected void SetProjectile(bool isOn) 
        => BossNairanBattlecruiserCtrl.Instance.BossNairanBattlecruiserShootShockWave.SetIsFiring(isOn);

    protected void SetAnimaiton(bool isOn)
        => BossNairanBattlecruiserCtrl.Instance.BossNairanBattlecruiserModelShipAnimation.SetIsArcShockWave(isOn);
    
    protected Vector3 GetStartPosition() => BossNairanBattlecruiserCtrl.Instance.GetStartPosition();
    protected  Vector3 GetEndPosition() => BossNairanBattlecruiserCtrl.Instance.GetEndPosition();
    protected void UnSetAniamtion() => BossNairanBattlecruiserCtrl.Instance.SetIsArcShockWave(false);
    protected int GetNumberLoop() => BossNairanBattlecruiserCtrl.Instance.NumberOfLoop;
    protected float GetSpeedArc() =>  BossNairanBattlecruiserCtrl.Instance.SpeedArcShockWave;
    protected float GetSpeedToStartPosition() => BossNairanBattlecruiserCtrl.Instance.SpeedGoToReadyPosition;
}
