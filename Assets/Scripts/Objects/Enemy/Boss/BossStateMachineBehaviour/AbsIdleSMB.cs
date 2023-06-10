using UnityEngine;

public abstract class AbsIdleSMB : StateMachineBehaviour
{
    [SerializeField] protected float speedMoveToPosDefault = 4f;
    [SerializeField] protected float timeWait;
    private float timer;
    private bool isStopTimer = false;
    
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeWait = GetTimeWait();
        this.Behaviour(animator);
    }

    protected virtual void Behaviour(Animator animator)
    {
        if (animator.transform.position != GetDefaultPosition())
        {
            MoveToDefaultPosition(animator, GetDefaultPosition());
            if (animator.transform.position == GetDefaultPosition())
                isStopTimer = false;
        }
        else
        {
            if (!isStopTimer) timer += Time.deltaTime;
            if (timer >= timeWait && !isStopTimer)
            {
                timer = 0;
                isStopTimer = true;
                this.ChangeState(animator);
            }
        }
    }

    private void MoveToDefaultPosition(Animator animator, Vector3 defaultPosition)
    {
        animator.transform.position = Vector3.MoveTowards(animator.transform.position,
            defaultPosition,
            speedMoveToPosDefault * Time.deltaTime);
    }

    protected virtual int GetRandomState(int numberOfState) => Random.Range(1, numberOfState + 1);
    protected abstract void ChangeState(Animator animator);
    protected abstract Vector3 GetDefaultPosition();
    protected abstract float GetTimeWait();
}
