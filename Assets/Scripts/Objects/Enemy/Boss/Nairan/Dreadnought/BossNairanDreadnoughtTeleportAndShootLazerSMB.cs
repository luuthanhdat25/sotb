using Player;
using UnityEngine;

namespace Objects.Enemy.Boss.Nairan.Dreadnought
{
    public class BossNairanDreadnoughtTeleportAndShootLazerSMB : StateMachineBehaviour
    {
        [SerializeField] protected float timeShootInOneTime;
        [SerializeField] protected float timeDelayTeleport;
        [SerializeField] protected int numberOfAttacks;
        [SerializeField] protected float speedToReadyPosition = 4f;
        private Vector3 readyPosition = new Vector3(0,0f,0);
        private float timer;
        private bool isStopTimer;
        private bool isReady = false;
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            timeShootInOneTime = GetTimeShootOneTime();
            timeDelayTeleport = GetTimeDelayTeleport();
            numberOfAttacks = GetNumberOfActack();
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            this.Behaviour(animator);
        }

        private void Behaviour(Animator animator)
        {
            if (!isReady)
            {
                if (animator.transform.position != readyPosition)
                {
                    animator.transform.position = Vector3.MoveTowards(animator.transform.position, readyPosition,
                        speedToReadyPosition * Time.deltaTime);
                    if (animator.transform.position == readyPosition)
                    {
                        isReady = true;
                        isStopTimer = false;
                        TransformToPlayerPosition(animator);
                    }
                }
            }
            else if(numberOfAttacks > 0)
            {
                if(numberOfAttacks > 1)SetProjectile(true);
                if (!isStopTimer) timer += Time.deltaTime;
                if (!isStopTimer && timer >= timeShootInOneTime && timer < timeShootInOneTime + timeDelayTeleport)
                {
                    SetProjectile(false);
                }else if (timer >= timeShootInOneTime + timeDelayTeleport)
                {
                    if(numberOfAttacks > 1) TransformToPlayerPosition(animator);
                    timer = 0;
                    numberOfAttacks--;
                }
            }
            else
            {
                isReady = false;
                isStopTimer = true;
                SetProjectile(false);
                UnSetAnimation();
            }
        }

        protected void TransformToPlayerPosition(Animator animator)
        {
            animator.transform.position =
                GetTargetPosition(PlayerCtrl.Instance.GetCurrentPosition(), animator.transform.position);
        }
        protected virtual Vector3 GetTargetPosition(Vector3 playerPos, Vector3 currentPos)
        {
            return new Vector3(playerPos.x, this.GetRandomYValue() + currentPos.y, playerPos.z);
        }
        
        protected virtual float GetRandomYValue() => Random.Range(0f, 1.7f);

        protected float GetTimeShootOneTime() 
            => BossNairanDreadnoughtCtrl.Instance.TimeShootInOneTime;
        
        protected float GetTimeDelayTeleport()
            => BossNairanDreadnoughtCtrl.Instance.TimeDelayTeleport;
        protected int GetNumberOfActack() 
            => BossNairanDreadnoughtCtrl.Instance.NumberOfShootAttacks;
        
        protected void SetProjectile(bool isOn)
        {
            if(isOn) 
                BossNairanDreadnoughtCtrl.Instance.BossShootLazer.SpawnLazerAfterTime(BossNairanDreadnoughtCtrl.Instance.TimeDelayBeforeShoot);
            else 
                BossNairanDreadnoughtCtrl.Instance.BossShootLazer.DespawnLazer();
        }

        protected void UnSetAnimation()
            => BossNairanDreadnoughtCtrl.Instance.SetIsTeleportAndShootLazer(false);
    }
}