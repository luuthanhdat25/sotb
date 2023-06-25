using System;
using System.Collections;
using DefaultNamespace;
using Enemy.Boss.Nairan.Miniboss.Boss.Battlecruiser;
using Objects.Enemy.Boss.Nairan.Dreadnought;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy.Boss.Nairan.Miniboss.Boss
{
    public class DoubleBossNairanCtrl : MonoBehaviour
    {
        public static DoubleBossNairanCtrl Instance { get; private set; }
        [SerializeField] private GameObject healthBar;
        [SerializeField] private bool isIdle2 = false;
        public bool Isdle2 => isIdle2;
        [SerializeField] private int stateNumber;
        private int bossDie = 0;
        public int StateNumber => stateNumber;
        private bool isInSMB = false;
        private bool isSlide;
        public bool IsSlide => isSlide;

        [Header("Idle 1 Behaviour")]
        [SerializeField] private float timeWaitIdleOne = 3f;
        [SerializeField] private Transform defaultPosOneBattlecruiser = null;
        [SerializeField] private Transform defaultPosOneDreadnought = null;
        
        
        [Header("Idle 2 Behaviour")]
        [SerializeField] private float timeWaitIdleTwo = 2f;
        private Vector3 defaultPosTwo = new Vector3(0f, 5f, 0f);

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

        private void Awake()
        {
            if(Instance != null) Debug.LogError("There is more than one BossNairanBattlecruiserCtrl instance");
            Instance = this;
        }

        private void OnEnable() => StartCoroutine(TurnOnHealthBar());

        private IEnumerator TurnOnHealthBar()
        {
            yield return new WaitForSeconds(1f);
            healthBar?.SetActive(true);  
        }

        private void Start() => isSlide = false;

        private void Update()
        {
            if (bossDie == 2)
            {
                GameManager.Instance.WinGame();
                bossDie++;
                return;
            }
            this.Behaviour();
        }

        private void Behaviour()
        {
            if (!isIdle2)
                DoubleBehaviour();
            else
                SingleBehaviour();
        }

        private void DoubleBehaviour()
        {
            if (!isInSMB && BossNairanDreadnoughtCtrl.Instance.IsInDefaultPosition() && BossNairanBattlecruiserCtrl.Instance.IsInDefaultPosition())
            {
                isInSMB = true;
                StartCoroutine(ChangeState(2, timeWaitIdleOne));
            }
        }
        
        private IEnumerator ChangeState(int numOfState, float timeWaitIdle)
        {
            stateNumber = 0;
            yield return new WaitForSeconds(timeWaitIdle - 0.1f);
            stateNumber = GetStateNumber(numOfState);
            Debug.Log("Double boss state: " + stateNumber);
            isInSMB = false;
        }

        private int GetStateNumber(int numberOfState)
        {
            if (numberOfState > 2) return GetRandomState(numberOfState);
            if (isSlide)
            {
                isSlide = false;
                return 1;
            }
            else
            {
                isSlide = true;
                return 2;
            }
        }

        public  int GetRandomState(int numberOfState) => Random.Range(1, numberOfState + 1);

        private void SingleBehaviour(){
            if (!isInSMB /*&& (BossNairanBattlecruiserCtrl.Instance.IsDead*/ /*|| BossNairanBattlecruiserCtrl.Instance.IsDead*/)
            {
                isInSMB = true;
                StartCoroutine(ChangeState(3, timeWaitIdleTwo));
            }
        }
        
        public void SwapDefaultPosition() 
            => (defaultPosOneBattlecruiser, defaultPosOneDreadnought) = (defaultPosOneDreadnought, defaultPosOneBattlecruiser);
        
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

        public void OneShipDead()
        {
            this.isIdle2 = true;
            bossDie++;
        }
    }
}