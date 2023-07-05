using System;
using Damage.RhythmScripts;
using Enemy.Boss;
using Enemy.Boss.Nairan.Miniboss;
using Enemy.Boss.Nairan.Miniboss.Boss;
using Enemy.Boss.Nairan.Miniboss.Boss.Battlecruiser;
using Objects.Enemy.AttackEnemy;
using UnityEngine;

namespace Objects.Enemy.Boss.Nairan.Dreadnought
{
    public class BossNairanDreadnoughtCtrl : AbsBossCtrl
    {
        public static BossNairanDreadnoughtCtrl Instance { get; private set; }
        [SerializeField] private BossNairanDreadnoughtModelShipAnimation bossModelShipAnimation;
        public BossNairanDreadnoughtModelShipAnimation BossNairanDreadnoughtModelShipAnimation => bossModelShipAnimation;
        [SerializeField] private BossShootLazer bossShootLazer;
        public BossShootLazer BossShootLazer => bossShootLazer;
        [SerializeField] private BossNairanDreadnoughtShootNormal bossNairanDreadnoughtShootNormal;
        public BossNairanDreadnoughtShootNormal BossNairanDreadnoughtShootNormal => bossNairanDreadnoughtShootNormal;
        [SerializeField] private BossNairanDreadnoughtSpawnTorpedo bossNairanDreadnoughtSpawnTorpedo;
        public BossNairanDreadnoughtSpawnTorpedo BossNairanDreadnoughtSpawnTorpedo => bossNairanDreadnoughtSpawnTorpedo;
        [SerializeField] private BossNairanDreadnoughtDamageReciever bossNairanDreadnoughtDamageReciever;
        public BossNairanDreadnoughtDamageReciever BossNairanDreadnoughtDamageReciever => bossNairanDreadnoughtDamageReciever;
        
        [SerializeField] private SpriteRenderer engineSpriteRenderer;
        [SerializeField] private PolygonCollider2D polygonCollider2D;
        
        private bool isDead = false;
        public bool IsDead => isDead;
        private bool isFinishBehaviour = false;
        public bool IsFinishBehaviour => isFinishBehaviour;
        
        public void SetIsFinishBehaviour(bool isFinish) => isFinishBehaviour = isFinish;

        private enum AnimatorParameter
        {
            IsIdle2,
            IsLazerSlide,
            IsFollowAndShootNormal,
            IsDestruction,
            IsArcShootNormal,
            IsSpawnTorpedo,
            IsTeleportAndShootLazer
        }
        
        [Header("ArcShootNormalBehaviour")]
        [SerializeField] private float speedArcShockWave = 1f;
        [SerializeField] private Transform startPosition;
        [SerializeField] private Transform endPosition;
        [SerializeField] private int numberOfLoop = 2;
        [SerializeField] private float speedGoToReadyPosition = 6;
        [SerializeField] private float firingRate = 0.4f;
        
        [Header("SpawnTorpedoBehaviour")]
        [SerializeField] private float spawnRate = 0.6f;
        [SerializeField] private float timeDelayBeforeSpawn = 1f;
        [SerializeField] private float timeSpawnTorpedo = 5f;

        [Header("TeleportAndShootLazerBehaviour")]
        [SerializeField] private int numberOfShootAttacks = 3;
        [SerializeField] private float timeDelayBeforeShoot = 1.5f;
        [SerializeField] private float timeShootInOneTime = 2f;
        [SerializeField] private float timeDelayTeleport = 0.5f;
        
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

        private void LoadBossShootLazer() => this.bossShootLazer ??= GetComponentInChildren<BossShootLazer>();

        private void LoadBossNairanDreadnoughtShootNormal() 
            => this.bossNairanDreadnoughtShootNormal ??= GetComponentInChildren<BossNairanDreadnoughtShootNormal>();

        private void LoadBossBossNairanDreadnoughtSpawnTorpedo() 
            => this.bossNairanDreadnoughtSpawnTorpedo ??= GetComponentInChildren<BossNairanDreadnoughtSpawnTorpedo>();

        private void LoadBossModelShipAnimationScript() 
            => this.bossModelShipAnimation ??= GetComponentInChildren<BossNairanDreadnoughtModelShipAnimation>();


        private void Update()
        {
            if(isDead) return;
            CheckIdle2Animation();
        }

        private void CheckIdle2Animation() 
            => bossSMBAnimator.SetBool(AnimatorParameter.IsIdle2.ToString(), DoubleBossNairanCtrl.Instance.Isdle2);
        
        public override void SetDeadAnimation()
        {
            this.isFinishBehaviour = true;
            this.bossShootLazer.DespawnLazer();
            EnemyProjectileSpawner.Instance.DespawnAllPool();
            
            PlayExplosionAudio();
            Invoke("PlayExplosionAudio", 0.2f);
            Invoke("PlayExplosionAudio", 0.5f);
            
            this.bossModelShipAnimation.SetIsDestructionTrigger();
            this.bossSMBAnimator.SetTrigger(AnimatorParameter.IsDestruction.ToString());
        }
        
        private void PlayExplosionAudio() => AudioSpawner.Instance.SpawnEnemyEffect(AudioSpawner.SoundEffectEnum.ExplosionBoss);

        public override Vector3 GetDefaultPosition()
        {
            if (DoubleBossNairanCtrl.Instance.Isdle2) 
                defaultPosition = DoubleBossNairanCtrl.Instance.DefaultPosTwo;
            else 
                defaultPosition = DoubleBossNairanCtrl.Instance.DefaultPosOneDreadnought.position;
            
            return defaultPosition;
        }

        public void SetIsLazerSlide(bool isTrue) 
            => this.bossSMBAnimator.SetBool(AnimatorParameter.IsLazerSlide.ToString(), isTrue);
        
        public void SetIsFollowAndShootNormal(bool isTrue)
            => this.bossSMBAnimator.SetBool(AnimatorParameter.IsFollowAndShootNormal.ToString(), isTrue);
        
        public void SetIsArcShootNormal(bool isTrue)
            => this.bossSMBAnimator.SetBool(AnimatorParameter.IsArcShootNormal.ToString(), isTrue);
        
        public void SetIsSpawnTorpedo(bool isTrue)
            => this.bossSMBAnimator.SetBool(AnimatorParameter.IsSpawnTorpedo.ToString(), isTrue);
        
        public void SetIsTeleportAndShootLazer(bool isTrue)
            => this.bossSMBAnimator.SetBool(AnimatorParameter.IsTeleportAndShootLazer.ToString(), isTrue);

        public float SpeedArcShockWave => speedArcShockWave;
        
        public Vector3 GetStartPosition() => startPosition.position;
        
        public Vector3 GetEndPosition() => endPosition.position;
        
        public int NumberOfLoop => numberOfLoop;
        
        public float SpawnRate => spawnRate;
        
        public float TimeDelayBeforeSpawn => timeDelayBeforeSpawn;
        
        public int NumberOfShootAttacks => numberOfShootAttacks;
        
        public float TimeDelayTeleport => timeDelayTeleport;
        
        public float TimeShootInOneTime => timeShootInOneTime;
        
        public float TimeDelayBeforeShoot => timeDelayBeforeShoot;
        
        public float SpeedGoToReadyPosition => speedGoToReadyPosition;
        
        public float FiringRate => firingRate;
        
        public float TimeSpawnTorpedo => timeSpawnTorpedo;
        
        public bool IsInDefaultPosition() => transform.position == defaultPosition;
        
        public void IsDeadTrue() => this.isDead = true;

        public void SetActiveEngine(bool active) => engineSpriteRenderer.enabled = active;
        
        public void SetActiveCollider(bool active) => polygonCollider2D.enabled = active;
    }
}