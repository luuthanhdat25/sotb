using Enemy.Boss;
using Enemy.Boss.Nairan.Miniboss;
using Enemy.Boss.Nairan.Miniboss.Boss;
using UnityEngine;

namespace Objects.Enemy.Boss.Nairan.Dreadnought
{
    public class BossNairanDreadnoughtCtrl : AbsBossCtrl
    {
        public static BossNairanDreadnoughtCtrl Instance { get; private set; }
        [SerializeField] private BossNairanDreadnoughtModelShipAnimation bossModelShipAnimation;
        [SerializeField] private BossShootLazer bossShootLazer;
        public BossShootLazer BossShootLazer => bossShootLazer;
        [SerializeField] private BossNairanDreadnoughtShootNormal bossNairanDreadnoughtShootNormal;
        public BossNairanDreadnoughtShootNormal BossNairanDreadnoughtShootNormal => bossNairanDreadnoughtShootNormal;
        [SerializeField] private BossNairanDreadnoughtSpawnTorpedo bossNairanDreadnoughtSpawnTorpedo;
        public BossNairanDreadnoughtSpawnTorpedo BossNairanDreadnoughtSpawnTorpedo => bossNairanDreadnoughtSpawnTorpedo;
        private bool isDead = false;
        public bool IsDead => isDead;

        private enum AnimatorParameter
        {
            IsIdle2,
            IsLazerSlide,
            IsFollowAndShootShockWave,
            IsDestruction,
            IsArcShockWave,
            IsRotateLazer,
            IsFollowAndShootLazer
        }
        
        [Header("ArcShockWaveBehaviour")]
        [SerializeField] private float speedArcShockWave = 1f;
        [SerializeField] private Transform startPosition;
        [SerializeField] private Transform endPosition;
        [SerializeField] private int numberOfLoop = 2;
        
        [Header("RotateLazerBehaviour")]
        [SerializeField] private float rotationSpeed = 60f;
        [SerializeField] private float speedGoToReadyPosition = 5f;
        [SerializeField] private float degreeRotate = 720;
        
        [Header("ShootLazerBehaviour")]
        [SerializeField] private int numberOfShootAttacks = 4;
        [SerializeField] private float speedFollow = 10f;
        [SerializeField] private float timeShootInOneTime = 2f;
        [SerializeField] private float timeDelayBeforeShoot = 1.5f;
        
        protected override void Awake()
        {
            base.Awake();
            if(Instance != null) Debug.LogError("There is more than one BossNairanBattlecruiserCtrl instance");
            Instance = this;
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadBossShootLazer();
            this.LoadBossNairanDreadnoughtShootNormal();
            this.LoadBossBossNairanDreadnoughtSpawnTorpedo();
            this.LoadBossModelShipAnimationScript();
        }

        private void LoadBossShootLazer()
        {
            if (this.bossShootLazer != null) return;
            this.bossShootLazer = GetComponentInChildren<BossShootLazer>();
            Debug.Log(transform.name + " Load: BossShootLazer");
        }
        
        private void LoadBossNairanDreadnoughtShootNormal()
        {
            if (this.bossNairanDreadnoughtShootNormal != null) return;
            this.bossNairanDreadnoughtShootNormal = GetComponentInChildren<BossNairanDreadnoughtShootNormal>();
            Debug.Log(transform.name + " Load: BossNairanDreadnoughtShootNormal");
        }
        
        private void LoadBossBossNairanDreadnoughtSpawnTorpedo()
        {
            if (this.bossNairanDreadnoughtSpawnTorpedo != null) return;
            this.bossNairanDreadnoughtSpawnTorpedo = GetComponentInChildren<BossNairanDreadnoughtSpawnTorpedo>();
            Debug.Log(transform.name + " Load: BossNairanDreadnoughtSpawnTorpedo");
        }
        
        private void LoadBossModelShipAnimationScript()
        {
            if (this.bossModelShipAnimation != null) return;
            this.bossModelShipAnimation = GetComponentInChildren<BossNairanDreadnoughtModelShipAnimation>();
            Debug.Log(transform.name + " Load: BossNairanDreadnoughtModelShipAnimation");
        }

        private void Update()
        {
            if(isDead) return;
            CheckIdle2Animation();
        }

        private void CheckIdle2Animation() 
            => bossSMBAnimator.SetBool(AnimatorParameter.IsIdle2.ToString(), DoubleBossNairanCtrl.Instance.Isdle2);
        
        public override void SetDeadAnimation()
        {
            this.bossModelShipAnimation.SetIsDestructionTrigger();
            this.bossShootLazer.DespawnLazer();
            Debug.Log("set destruction");
            this.bossSMBAnimator.SetTrigger(AnimatorParameter.IsDestruction.ToString());
        }

        public override Vector3 GetDefaultPosition()
        {
            if (DoubleBossNairanCtrl.Instance.Isdle2) 
                defaultPosition = DoubleBossNairanCtrl.Instance.DefaultPosTwo;
            else 
                defaultPosition = DoubleBossNairanCtrl.Instance.DefaultPosOneBattlecruiser.position;
            
            return defaultPosition;
        }

        public void SetIsLazerSlide(bool isTrue) 
            => this.bossSMBAnimator.SetBool(AnimatorParameter.IsLazerSlide.ToString(), isTrue);
        
        public void SetIsFollowAndShootShockWave(bool isTrue)
            => this.bossSMBAnimator.SetBool(AnimatorParameter.IsFollowAndShootShockWave.ToString(), isTrue);
        
        public void SetIsArcShockWave(bool isTrue)
            => this.bossSMBAnimator.SetBool(AnimatorParameter.IsArcShockWave.ToString(), isTrue);
        
        public void SetIsRotateLazer(bool isTrue)
            => this.bossSMBAnimator.SetBool(AnimatorParameter.IsRotateLazer.ToString(), isTrue);
        
        public void SetIsFollowAndShootLazer(bool isTrue)
            => this.bossSMBAnimator.SetBool(AnimatorParameter.IsFollowAndShootLazer.ToString(), isTrue);
        
        public float SpeedArcShockWave => speedArcShockWave;
        public Vector3 GetStartPosition() => startPosition.position;
        public Vector3 GetEndPosition() => endPosition.position;
        public float RotationSpeed => rotationSpeed;
        public int NumberOfShootAttacks => numberOfShootAttacks;
        public float SpeedFollow => speedFollow;
        public float TimeShootInOneTime => timeShootInOneTime;
        public float TimeDelayBeforeShoot => timeDelayBeforeShoot;
        public float SpeedGoToReadyPosition => speedGoToReadyPosition;
        public float DegreeRotate => degreeRotate;
        public int NumberOfLoop => numberOfLoop;
        public bool IsInDefaultPosition() => transform.position == defaultPosition;
        public void IsDeadTrue() => isDead = true;
    }
}