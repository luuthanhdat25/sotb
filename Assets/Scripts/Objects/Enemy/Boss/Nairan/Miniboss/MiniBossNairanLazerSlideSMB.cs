using Enemy.Boss.Nairan.Miniboss;
using Player;
using UnityEngine;

public class MiniBossNairanLazerSlideSMB : StateMachineBehaviour
{
    [SerializeField] protected float timeDelayBeforeSlide;
    [SerializeField] protected float speedSlide;
    [SerializeField] protected float speedFollow;
    [SerializeField] private float distanceLimit = 14f;
    private Vector3 targetPosition = Vector3.zero;
    private float timer;
    private bool isStopTimer = true;
    private Vector3 outVector;
    private float currentDistance = 0f;
    private bool isSlideGoOut = false;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        targetPosition = GetRamDomTargetPosition(animator.transform.position);
        speedSlide = GetSpeedSlide();
        timeDelayBeforeSlide = GetTimeDelayBeforeSlide();
        speedFollow = GetSpeedFollow();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        this.Behaviour(animator);
    }

    private void Behaviour(Animator animator)
    {
        if (!isSlideGoOut && !IsGoOutScreen(animator))
        {
            if (animator.transform.position != targetPosition && isStopTimer)
            {
                MiniBossNairanCtrl.Instance.MinibossNairanModelShipAnimation.SetIsShootLazer(true);
                MoveTowardsTo(targetPosition, speedFollow, animator);
                if (animator.transform.position == targetPosition) isStopTimer = false;
            }
            else
            {
                //Wait for time Delay Before Slide
                if (!isStopTimer) timer += Time.deltaTime;
                if (!isStopTimer && timer >= timeDelayBeforeSlide)
                {
                    //Slide to Player
                    SetProjectile(true);
                    ResetOutVector(animator);
                    MoveTowardsTo(outVector, speedSlide, animator);
                }
            }
        }
        else
        {
            if (!isSlideGoOut) TransformToContraryDirectMove(animator);
            isSlideGoOut = true;
            MoveTowardsTo(targetPosition, speedSlide, animator);
            if (animator.transform.position == targetPosition)
            {
                timer = 0; isStopTimer = true;
                outVector = Vector3.zero;
                isSlideGoOut = false;
                SetProjectile(false);
                MiniBossNairanCtrl.Instance.MinibossNairanModelShipAnimation.SetIsShootLazer(false);
                UnSetAnimation();
            }
        }
    }
    
    private void MoveTowardsTo(Vector3 targetVector3, float speed, Animator animator)
    {
        animator.transform.position = Vector3.MoveTowards(animator.transform.position,
            targetVector3, speed * Time.deltaTime);
    }
    
    private bool IsGoOutScreen(Animator animator)
    {
        this.currentDistance = Vector3.Distance(animator.transform.position, MiniBossNairanCtrl.Instance.GetCameraPosition());
        if (this.currentDistance > distanceLimit) return true;
        return false;
    }
    
    private void ResetOutVector(Animator animator)
    {
        if (this.outVector != Vector3.zero) return;

        //float dirX = 15f;
        float dirY = animator.transform.position.y;
        if (animator.transform.position.x < GetPlayerPosition().x)
            outVector = new Vector3(distanceLimit, dirY);
        else
            outVector = new Vector3(-distanceLimit, dirY);    
    }

    private void TransformToContraryDirectMove(Animator animator)
    {
        if (outVector.x >= 0) animator.transform.position = new Vector3(-8, animator.transform.position.y, animator.transform.position.z);
        else animator.transform.position = new Vector3(8, animator.transform.position.y, animator.transform.position.z);
    }

    protected virtual Vector3 GetRamDomTargetPosition(Vector3 currentPos)
    {
        return new Vector3(GetPlayerPosition().x, this.GetRandomYValue() + currentPos.y, GetPlayerPosition().z);
    }

    private Vector3 GetPlayerPosition() => PlayerCtrl.Instance.GetCurrentPosition();

    protected virtual float GetRandomYValue() => Random.Range(-2f, 2f);

    protected float GetSpeedSlide() => MiniBossNairanCtrl.Instance.GetSpeedSlide();
    protected float GetSpeedFollow() => MiniBossNairanCtrl.Instance.GetSpeedFollow();
    protected float GetTimeDelayBeforeSlide() => MiniBossNairanCtrl.Instance.GetTimeDelayBeforeSlide();

    protected void SetProjectile(bool isOn)
    {
        if(isOn) MiniBossNairanCtrl.Instance.BossShootLazer.SpawnLazerAfterTime(0);
        else MiniBossNairanCtrl.Instance.BossShootLazer.DespawnLazer();
    }

    protected void UnSetAnimation() => MiniBossNairanCtrl.Instance.SetIsLazerSlide(false);
}
