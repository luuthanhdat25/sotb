using Damage.RhythmScripts;
using UnityEngine;

namespace Enemy.Boss.Nairan.Miniboss
{
    public class MiniBossNairanCtrl : RepeatMonoBehaviour
    {
        public static MiniBossNairanCtrl Instance { get; private set; } 
        private Vector3 defaultPosition = new Vector3(0f, 5f, 0f);
        [SerializeField] protected Transform mainCam;
        [SerializeField] private Animator miniNairanAnimator;
        [SerializeField] private MinibossNairanModelShipAnimation minibossNairanModelShipAnimation;
        public MinibossNairanModelShipAnimation MinibossNairanModelShipAnimation => minibossNairanModelShipAnimation;
        [SerializeField] private MinibossNairanShootNormal minibossNairanShootNormal;
        public MinibossNairanShootNormal MinibossNairanShootNormal { get => minibossNairanShootNormal; }
        [SerializeField] private BossShootLazer bossShootLazer;
        public BossShootLazer BossShootLazer { get => bossShootLazer; }

        private const string IS_DESTRUCTION = "isDestruction";
        private const string IS_FOLLOW_AND_SHOOT = "isFollowAndShoot";
        private const string IS_LAZER_SLIDE = "isLazerSlide";
        
        [Header("IdleBehaviour")]
        [SerializeField] private float timeWait = 2f;

        [Header("ShootBehaviour")]
        [SerializeField] private int numberOfShootAttacks = 1;
        [SerializeField] private float speedFollow = 6f;
        [SerializeField] private float timeShootInOneTime = 5f;
        
        [Header("LazerBehaviour")]
        [SerializeField] private float speedSlide = 7f;
        [SerializeField] private float timeDelayBeforeSlide = 1f;

        protected override void Awake()
        {
            if(Instance != null) Debug.LogError("There is more than one MinibossNairan instance");
            Instance = this;
        }

        /*private void Start()
        {
            StartCoroutine(AfterOnMinute());
        }

        private IEnumerator AfterOnMinute()
        {
            yield return new WaitForSeconds(60);
            numberOfShootAttacks = 5;
            speedFollow = 10;
            timeShootInOneTime = 1;
            numberOfChaseAttacks = 3;
            speedChase = 12;
            timeWait = 1f;
        }*/

        public void SetDeadAnimation()
        {
            this.bossShootLazer.DespawnLazer();
            
            PlayExplosionAudio();
            Invoke("PlayExplosionAudio", 0.2f);
            Invoke("PlayExplosionAudio", 0.5f);
            
            this.minibossNairanModelShipAnimation.SetIsDestructionTrigger();
            this.miniNairanAnimator.SetTrigger(IS_DESTRUCTION);
        }
        
        private void PlayExplosionAudio() => AudioSpawner.Instance.SpawnEnemyEffect(AudioSpawner.SoundEffectEnum.ExplosionBoss);

        public void SetIsFollowAndShoot(bool isTrue) => this.miniNairanAnimator.SetBool(IS_FOLLOW_AND_SHOOT, isTrue);
        public void SetIsLazerSlide(bool isTrue) => this.miniNairanAnimator.SetBool(IS_LAZER_SLIDE, isTrue);
        public Vector3 GetCameraPosition() => this.mainCam.position;
        public Vector3 GetDefaultPosition() => this.defaultPosition;
        public int GetNumberOfAttacksFollowAndShootBehaviour() => this.numberOfShootAttacks;
        public float GetSpeedFollow() => this.speedFollow;
        public float GetTimeShootOneTime() => this.timeShootInOneTime;
        public float GetSpeedSlide() => this.speedSlide;
        public float GetTimeDelayBeforeSlide() => this.timeDelayBeforeSlide;
        public float GetTimeWaitIdle() => this.timeWait;
    }
}