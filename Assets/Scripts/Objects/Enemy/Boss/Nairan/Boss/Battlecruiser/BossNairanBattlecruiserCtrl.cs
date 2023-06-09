using UnityEngine;

namespace Enemy.Boss.Nairan.Miniboss.Boss.Battlecruiser
{
    public class BossNairanBattlecruiserCtrl : AbsBossCtrl
    {
        public static BossNairanBattlecruiserCtrl Instance { get; private set; }
        [SerializeField] private BossNairanBattlecruiserModelShipAnimation bossModelShipAnimation;
        [SerializeField] private BossShootLazer bossShootLazer;
        public BossShootLazer BossShootLazer => bossShootLazer;
        [SerializeField] private BossNairanBattlecruiserShootShockWave bossNairanBattlecruiserShootShockWave;
        public BossNairanBattlecruiserShootShockWave BossNairanBattlecruiserShootShockWave => bossNairanBattlecruiserShootShockWave;

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
        [SerializeField] private float speedArcShockWave = 7f;
        [SerializeField] private Transform startPosition;
        [SerializeField] private Transform endPosition;
        
        [Header("RotateLazerBehaviour")]
        [SerializeField] private float speedRotate = 10f;
        
        [Header("ShootLazerBehaviour")]
        [SerializeField] private int numberOfShootAttacks = 4;
        [SerializeField] private float speedFollow = 10f;
        [SerializeField] private float timeShootInOneTime = 2f;
        
        private void Awake()
        {
            if(Instance != null) Debug.LogError("There is more than one BossNairanBattlecruiserCtrl instance");
            Instance = this;
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadBossShootLazer();
            this.LoadBossShootShockWave();
            this.LoadBossModelShipAnimationScript();
        }

        private void LoadBossShootLazer()
        {
            if (this.bossShootLazer != null) return;
            this.bossShootLazer = GetComponentInChildren<BossShootLazer>();
            Debug.Log(transform.name + " Load: BossShootLazer");
        }
        
        private void LoadBossShootShockWave()
        {
            if (this.bossNairanBattlecruiserShootShockWave != null) return;
            this.bossNairanBattlecruiserShootShockWave = GetComponentInChildren<BossNairanBattlecruiserShootShockWave>();
            Debug.Log(transform.name + " Load: bossNairanBattlecruiserShootShockWave");
        }
        
        private void LoadBossModelShipAnimationScript()
        {
            if (this.bossModelShipAnimation != null) return;
            this.bossModelShipAnimation = GetComponentInChildren<BossNairanBattlecruiserModelShipAnimation>();
            Debug.Log(transform.name + " Load: BossNairanBattlecruiserModelShipAnimation");
        }

        public override void SetDeadAnimation()
        {
            this.bossModelShipAnimation.SetIsDestructionTrigger();
            this.bossShootLazer.DespawnLazer();
            Debug.Log("set destruction");
            this.bossSMBAnimator.SetTrigger(AnimatorParameter.IsDestruction.ToString());
        }

        //public void SetIsFollowAndShoot(bool isTrue) => this.miniNairanAnimator.SetBool(IS_FOLLOW_AND_SHOOT, isTrue);
        //public void SetIsLazerSlide(bool isTrue) => this.miniNairanAnimator.SetBool(IS_LAZER_SLIDE, isTrue);
        public float SpeedArcShockWave => speedArcShockWave;

        public Transform StartPosition => startPosition;

        public Transform EndPosition => endPosition;

        public float SpeedRotate => speedRotate;

        public int NumberOfShootAttacks => numberOfShootAttacks;

        public float SpeedFollow => speedFollow;

        public float TimeShootInOneTime => timeShootInOneTime;
    }
}