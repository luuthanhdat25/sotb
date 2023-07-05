using System;
using Damage.RhythmScripts;
using Objects.Enemy.Boss.Nairan.Dreadnought;
using UnityEngine;

namespace Enemy.Boss.Nairan.Miniboss.Boss.Battlecruiser
{
    public class BossNairanBattlecruiserCtrl : AbsBossCtrl
    {
        public static BossNairanBattlecruiserCtrl Instance { get; private set; }
        [SerializeField] private BossNairanBattlecruiserModelShipAnimation bossModelShipAnimation;
        public BossNairanBattlecruiserModelShipAnimation BossNairanBattlecruiserModelShipAnimation => bossModelShipAnimation;
        [SerializeField] private BossShootLazer bossShootLazer;
        public BossShootLazer BossShootLazer => bossShootLazer;
        [SerializeField] private BossNairanBattlecruiserShootShockWave bossNairanBattlecruiserShootShockWave;
        public BossNairanBattlecruiserShootShockWave BossNairanBattlecruiserShootShockWave => bossNairanBattlecruiserShootShockWave;
        [SerializeField] private BossNairanBattlecruiserDamageReciever bossNairanBattlecruiserDamageReciever;
        public BossNairanBattlecruiserDamageReciever BossNairanBattlecruiserDamageReciever => bossNairanBattlecruiserDamageReciever;

        private bool isDead = false;
        public bool IsDead => isDead;
        private bool isFinishBehaviour = false;
        public bool IsFinishBehaviour => isFinishBehaviour;
        public void SetIsFinishBehaviour(bool isFinish) => isFinishBehaviour = isFinish;

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
            this.LoadBossShootShockWave();
            this.LoadBossModelShipAnimationScript();
        }

        private void LoadBossShootLazer() => this.bossShootLazer ??= GetComponentInChildren<BossShootLazer>();

        private void LoadBossShootShockWave() 
            => this.bossNairanBattlecruiserShootShockWave ??= GetComponentInChildren<BossNairanBattlecruiserShootShockWave>();

        private void LoadBossModelShipAnimationScript() 
            => this.bossModelShipAnimation ??= GetComponentInChildren<BossNairanBattlecruiserModelShipAnimation>();

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