using Player;
using UnityEngine;

namespace Objects.Enemy.Boss.Nairan.Dreadnought
{
    public class BossNairanDreadnoughtTeleportAndShootLazerSMB : StateMachineBehaviour
    {
        [SerializeField] protected float timeDelayBeforeShoot;
        [SerializeField] protected float outHoleTimeAnimation = 0.5f;
        [SerializeField] protected float timeShootInOneTime;
        [SerializeField] protected float timeDelayTeleport;
        [SerializeField] protected int numberOfAttacks;
        [SerializeField] protected float speedToReadyPosition = 4f;
        private Vector3 readyPosition = new Vector3(0,0f,0);
        private float timer;
        private bool isReady = false;
        private bool isOutHole = false;
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            timeDelayBeforeShoot = GetTimeDelayBeforeShoot();
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
                    SetIntoHoleAnimation(true);
                    BossNairanDreadnoughtCtrl.Instance.SetActiveCollider(false);
                    BossNairanDreadnoughtCtrl.Instance.SetActiveEngine(false);
                    animator.transform.position = Vector3.MoveTowards(animator.transform.position, readyPosition,
                        speedToReadyPosition * Time.deltaTime);
                    if (animator.transform.position == readyPosition)
                    {
                        isReady = true;
                        TransformToPlayerPosition(animator);
                    }
                }
            }
            else if(numberOfAttacks > 0)
            {
                if (!isOutHole)
                {
                    SetOutHoleAnimation(true);
                    BossNairanDreadnoughtCtrl.Instance.SetActiveCollider(true);
                    isOutHole = true;
                }
                
                timer += Time.deltaTime;
                
                if (timer >= outHoleTimeAnimation && timer < timeDelayBeforeShoot)
                {
                    SetShootAnimation(true);
                    BossNairanDreadnoughtCtrl.Instance.SetActiveEngine(true);
                }else if (timer >= timeDelayBeforeShoot && timer < timeShootInOneTime)
                {
                    SetProjectile(true);
                } else if (timer >= timeShootInOneTime && timer < timeShootInOneTime + timeDelayTeleport)
                {
                    SetProjectile(false);
                    SetShootAnimation(false);
                    SetIntoHoleAnimation(false);
                    SetOutHoleAnimation(false);
                    
                    if(numberOfAttacks == 1) numberOfAttacks--;
                    
                    if (numberOfAttacks > 1)
                    {
                        BossNairanDreadnoughtCtrl.Instance.SetActiveEngine(false);
                        SetIntoHoleAnimation(true);
                        BossNairanDreadnoughtCtrl.Instance.SetActiveCollider(false);
                    }
                }else if (timer >= timeShootInOneTime + timeDelayTeleport)
                {
                    TransformToPlayerPosition(animator);
                    timer = 0;
                    isOutHole = false;
                    numberOfAttacks--;
                }
            }
            else
            {
                BossNairanDreadnoughtCtrl.Instance.SetActiveEngine(true);
                BossNairanDreadnoughtCtrl.Instance.SetActiveCollider(true);
                timer = 0;
                isReady = false;
                isOutHole = false;
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

        protected float GetTimeDelayBeforeShoot()
            => BossNairanDreadnoughtCtrl.Instance.TimeDelayBeforeShoot;
        
        protected void SetProjectile(bool isOn)
        {
            if(isOn) 
                BossNairanDreadnoughtCtrl.Instance.BossShootLazer.SpawnLazerAfterTime(0);
            else 
                BossNairanDreadnoughtCtrl.Instance.BossShootLazer.DespawnLazer();
        }

        protected void SetIntoHoleAnimation(bool isOn) => BossNairanDreadnoughtCtrl.Instance.BossNairanDreadnoughtModelShipAnimation.SetIsIntoHole(isOn);

        protected void SetOutHoleAnimation(bool isOn) => BossNairanDreadnoughtCtrl.Instance.BossNairanDreadnoughtModelShipAnimation.SetIsOutHole(isOn);

        protected void SetShootAnimation(bool isOn) => BossNairanDreadnoughtCtrl.Instance.BossNairanDreadnoughtModelShipAnimation.SetIsTeleportAndShootLazer(isOn);

        protected void UnSetAnimation()
            => BossNairanDreadnoughtCtrl.Instance.SetIsTeleportAndShootLazer(false);
    }
}