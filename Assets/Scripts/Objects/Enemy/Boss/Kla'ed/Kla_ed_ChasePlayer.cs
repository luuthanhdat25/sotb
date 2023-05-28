using Player;
using UnityEngine;
using Random = UnityEngine.Random;

public class Kla_ed_ChasePlayer : StateMachineBehaviour
{
    [SerializeField] private float rateOfIncreaseSpeed = 1.5f;
    [SerializeField] private float distanceLimit = 14f;
    private Vector3 targetPosition = Vector3.zero;
    private float speedChase;
    private bool isGoOverPlayer;
    private int numberOfAttacks;
    private float currentDistance = 0f;
    private Vector3 outVector;
    public override  void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isGoOverPlayer = false;
        numberOfAttacks = MiniBossKla_edCtrl.Instance.GetNumberOfAttacksChaseBehaviour();
        speedChase = MiniBossKla_edCtrl.Instance.GetSpeedChase();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (numberOfAttacks != 0) Behaviour(animator);
        else MiniBossKla_edCtrl.Instance.SetIsChasePlayer(false);
    }
    
    private void Behaviour(Animator animator)
    {
        AssignTargetPositionIfEqualVectorZero();
        MoveToTarget(animator);
    }

    private void AssignTargetPositionIfEqualVectorZero()
    {
        if (this.targetPosition != Vector3.zero) return;
        this.targetPosition = PlayerCtrl.Instance.GetCurrentPosition();
    }
    
    private void MoveToTarget(Animator animator)
    {
        if (!IsGoOutScreen(animator))
        {
            if(!isGoOverPlayer)
            {
                MoveTowardsTo(targetPosition, animator);
                if (animator.transform.position == targetPosition)
                    isGoOverPlayer = true;
            }
            else
            {
                ResetOutVector();
                MoveTowardsTo(outVector, animator);
            }
        }
        else
        {
            numberOfAttacks--;
            isGoOverPlayer = false;
            ResetTargetPosition();
            TransformToTopScreen(animator);
            outVector = Vector3.zero;
        }
    }

    private void MoveTowardsTo(Vector3 vectorMove, Animator animator)
    {
        animator.transform.position = Vector3.MoveTowards(animator.transform.position,
            vectorMove, speedChase * Time.deltaTime);
    }

    private void ResetOutVector()
    {
        if (this.outVector != Vector3.zero) return;
        float getRandomX = Random.Range(-10f, 10f);
        float getRandomY = Random.Range(-17f, -13f);
        this.outVector = new Vector3(getRandomX, getRandomY);
    }
    
    private bool IsGoOutScreen(Animator animator)
    {
        this.currentDistance = Vector3.Distance(animator.transform.position, MiniBossKla_edCtrl.Instance.GetCameraPosition());
        if (this.currentDistance > distanceLimit) return true;
        return false;
    }
    
    private void TransformToTopScreen(Animator animator)
    {
        if (outVector.x >= 0) animator.transform.position = new Vector3(-outVector.x, 9.5f, outVector.z);
        else animator.transform.position = new Vector3(-outVector.x, 9.5f, outVector.z);
        
    }
    
    private void ResetTargetPosition() => this.targetPosition = PlayerCtrl.Instance.GetCurrentPosition();
}
