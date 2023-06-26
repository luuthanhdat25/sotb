using System;
using System.Collections;
using System.Collections.Generic;
using Enemy.Boss.Nairan.Miniboss.Boss.Battlecruiser;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossNairanBattlecruiserRotateLazerSMB : StateMachineBehaviour
{
    [SerializeField] protected float rotationSpeed;
    [SerializeField] protected float speedGotoReadyPosition;
    [SerializeField] protected float degreeRotate; 
    
    private Vector3 targetPosition = Vector3.zero;
    private float timer;
    private bool isStopTimer; 
    private float totalRotation = 0f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rotationSpeed = GetRotationSpeed();
        speedGotoReadyPosition = GetSpeedGotoReadyPosition();
        degreeRotate = GetDegreeRotate();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        this.Behaviour(animator);
    }

    private void Behaviour(Animator animator)
    {
        if (animator.transform.position != targetPosition)
        {
            animator.transform.position = Vector3.MoveTowards(animator.transform.position, targetPosition,
                speedGotoReadyPosition * Time.deltaTime);
            if (animator.transform.position == targetPosition)
            {
                SetProjectile(true);
                SetAnimation(true);
                isStopTimer = false;
            }
        }
        else
        {
            if (!isStopTimer) timer += Time.deltaTime;
            if (!isStopTimer && timer >= BossNairanBattlecruiserCtrl.Instance.TimeDelayBeforeShoot)
            {
                animator.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
                rotationSpeed += (rotationSpeed * Time.deltaTime) / 3;
                totalRotation += Mathf.Abs(rotationSpeed * Time.deltaTime);
                if (totalRotation >= degreeRotate)
                {
                    animator.transform.rotation = Quaternion.identity;
                    timer = 0; isStopTimer = true;
                    totalRotation = 0;
                    SetProjectile(false);
                    SetAnimation(false);
                    UnSetAnimation();
                }
            }
        }
    }
    
    protected float GetRotationSpeed() => BossNairanBattlecruiserCtrl.Instance.RotationSpeed;
   
    protected float GetSpeedGotoReadyPosition() => BossNairanBattlecruiserCtrl.Instance.SpeedGoToReadyPosition;

    protected virtual float GetDegreeRotate() => BossNairanBattlecruiserCtrl.Instance.DegreeRotate;
    
    protected void SetProjectile(bool isOn)
    {
        if (isOn)
            BossNairanBattlecruiserCtrl.Instance.BossShootLazer.SpawnLazerAfterTime(BossNairanBattlecruiserCtrl.Instance.TimeDelayBeforeShoot);
        else
            BossNairanBattlecruiserCtrl.Instance.BossShootLazer.DespawnLazer();
    }

    protected void SetAnimation(bool isOn)
        => BossNairanBattlecruiserCtrl.Instance.BossNairanBattlecruiserModelShipAnimation.SetIsLazerSlide(isOn);

    protected void UnSetAnimation() => BossNairanBattlecruiserCtrl.Instance.SetIsRotateLazer(false);
    
}
