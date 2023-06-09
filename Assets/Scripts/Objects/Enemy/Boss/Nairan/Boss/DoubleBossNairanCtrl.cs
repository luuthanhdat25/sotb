using System;
using UnityEngine;

namespace Enemy.Boss.Nairan.Miniboss.Boss
{
    public class DoubleBossNairanCtrl : RepeatMonoBehaviour
    {
        public static DoubleBossNairanCtrl Instance { get; private set; }
        [SerializeField] protected Animator bossSMBAnimator;
        
        [Header("Idle 1 Behaviour")]
        [SerializeField] private float timeWaitIdleOne = 3f;
        [SerializeField] private Transform defaultPosOneBattlecruiser = null;
        [SerializeField] private Transform defaultPosOneDreadnought = null;
        
        [Header("Idle 2 Behaviour")]
        [SerializeField] private float timeWaitIdleTwo = 2f;
        private Vector3 defaultPosTwo;

        [Header("LazerSlideBehaviour")]
        [SerializeField] private float speedSlide = 6f;
        [SerializeField] private float timeDelayBeforeSlide = 1.5f;
        
        [Header("FollowAndShootShockWaveBattlecruiserBehaviour")]
        [SerializeField] private int numOfShootAttackSWBattlecruiser = 4;
        [SerializeField] private float speedFollowSWBattlecruiser = 7f;
        [SerializeField] private float timeShootOneTimeSWBattlecruiser = 2f;

        [Header("FollowAndShootNormalNormalBehaviour")]
        [SerializeField] private int numOfShootAttackNDreadnought = 6;
        [SerializeField] private float speedFollowNDreadnought = 7f;
        [SerializeField] private float timeShootOneTimeNDreadnought = 1f;
        protected override void Awake()
        {
            base.Awake();
            if(Instance != null) Debug.LogError("There is more than one BossNairanBattlecruiserCtrl instance");
            Instance = this;
        }
        
        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadAnimator();
        }
        
        protected virtual void LoadAnimator()
        {
            if (this.bossSMBAnimator != null) return;
            this.bossSMBAnimator = GetComponent<Animator>();
            Debug.Log(transform.name + " Load: bossSMBAnimator" );
        }

        private void Start()
        {
            defaultPosTwo = new Vector3(0f, 5f, 0f);
        }

        public float TimeWaitIdleOne => timeWaitIdleOne;

        public Transform DefaultPosOneBattlecruiser => defaultPosOneBattlecruiser;

        public Transform DefaultPosOneDreadnought => defaultPosOneDreadnought;

        public float TimeWaitIdleTwo => timeWaitIdleTwo;

        public Vector3 DefaultPosTwo => defaultPosTwo;

        public float SpeedSlide => speedSlide;

        public float TimeDelayBeforeSlide => timeDelayBeforeSlide;

        public int NumOfShootAttackSWBattlecruiser => numOfShootAttackSWBattlecruiser;

        public float SpeedFollowSWBattlecruiser => speedFollowSWBattlecruiser;

        public float TimeShootOneTimeSWBattlecruiser => timeShootOneTimeSWBattlecruiser;

        public int NumOfShootAttackNDreadnought => numOfShootAttackNDreadnought;

        public float SpeedFollowNDreadnought => speedFollowNDreadnought;

        public float TimeShootOneTimeNDreadnought => timeShootOneTimeNDreadnought;
    }
}