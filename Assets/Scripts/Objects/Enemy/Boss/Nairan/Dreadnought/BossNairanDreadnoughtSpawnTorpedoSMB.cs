using UnityEngine;

namespace Objects.Enemy.Boss.Nairan.Dreadnought
{
    public class BossNairanDreadnoughtSpawnTorpedoSMB : StateMachineBehaviour
    {
        [SerializeField] private float timeDelayBeforeSpawn = 1f;
        private float timeSpawnTorpedo;
        private float speedToReadyPosition = 3f;
        private Vector3 readyPosition = Vector3.zero;
        private float timer;
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
            => timeSpawnTorpedo = GetTimeSpawnTorpedo();

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
            => this.Behaviour(animator);

        private void Behaviour(Animator animator)
        {
            if (animator.transform.position != readyPosition)
            {
                animator.transform.position = Vector3.MoveTowards(animator.transform.position, readyPosition,
                    speedToReadyPosition * Time.deltaTime);
            }
            else
            {
                timer += Time.deltaTime;
                if (timer >= timeDelayBeforeSpawn && timer < timeSpawnTorpedo)
                {
                    SetProjectile(true);
                }else if (timer >= timeSpawnTorpedo)
                {
                    timer = 0;
                    SetProjectile(false);
                    UnSetAnimation();
                }
            }
        }

        protected float GetTimeSpawnTorpedo() 
            => BossNairanDreadnoughtCtrl.Instance.TimeSpawnTorpedo;

        protected void SetProjectile(bool isOn) 
            => BossNairanDreadnoughtCtrl.Instance.BossNairanDreadnoughtSpawnTorpedo.SetIsFiring(isOn);

        protected  void UnSetAnimation() 
            => BossNairanDreadnoughtCtrl.Instance.SetIsSpawnTorpedo(false);
    }
}