using System;
using System.Collections;
using Enemy.Nautolan;
using UnityEngine;

public class BossNautolanCtrl : RepeatMonoBehaviour
{
    public static BossNautolanCtrl Instance { get; private set; }
    [SerializeField] private GameObject healthBar;
    private Vector3 defaultPosition = new Vector3(0f, 5f, 0f);
    [Header("Component")]
    [SerializeField] private Animator nautolanAnimator;
    [SerializeField] private BossNautolanModelShipAnimation bossNautolanModelShipAnimation;
    public BossNautolanModelShipAnimation BossNautolanModelShipAnimation => bossNautolanModelShipAnimation;
    [SerializeField] private MinibossNautolanShoot minibossNautolanShoot;
    public MinibossNautolanShoot MinibossNautolanShoot { get => minibossNautolanShoot;}
    [SerializeField] private BossNautolanBomShoot bossNautolanBomShoot;
    public BossNautolanBomShoot BossNautolanBomShoot { get => bossNautolanBomShoot; }
    [SerializeField] private BossNautolanSpinningBulletShoot bossNautolanSpinningBulletShoot;
    public BossNautolanSpinningBulletShoot BossNautolanSpinningBulletShoot {get => bossNautolanSpinningBulletShoot;}
    [SerializeField] private BossNautolanShockWaveShoot bossNautolanShockWaveShoot;
    public BossNautolanShockWaveShoot BossNautolanShockWaveShoot { get => bossNautolanShockWaveShoot;}
    [SerializeField] private BossNautolanDamageReciever bossNautolanDamageReciever;
    public BossNautolanDamageReciever BossNautolanDamageReciever { get => bossNautolanDamageReciever; }

    private const string IS_DESTRUCTION = "isDestruction";
    private const string IS_FOLLOW_AND_SHOOT = "isFollowAndShoot";
    private const string IS_ARC_SHOOT = "isArcShoot";
    private const string IS_BOM_DROP = "isBomDrop";
    private const string IS_SHOCK_WAVE = "isShockWave";
    
    [Header("IdleBehaviour")]
    [SerializeField] private float timeWait = 2f;

    [Header("FollowAndShootBehaviour")]
    [SerializeField] private int numberOfShootAttacks = 1;
    [SerializeField] private float speedFollow = 6f;
    [SerializeField] private float timeShootInOneTime = 5f;

    [Header("ArcShootBehaviour")] 
    [SerializeField] private float curveHeight = 3f; 
    [SerializeField] protected float speedArc = 1f;
    [SerializeField] protected float speedToStartPosition = 6.5f;
    [SerializeField] private Transform startPoint;    
    [SerializeField] private Transform endPoint;    
    
    [Header("ShockWaveShootBehaviour")]
    [SerializeField] private int numberOfShootAttacksShockWave = 2;
    [SerializeField] private float speedFollowShockWave = 6f;
    [SerializeField] private float timeShootInOneTimeShockWave = 1.5f;

    protected override void Awake()
    {
        if(Instance != null) Debug.LogError("There is more than one BossNautolanCtrl instance");
        Instance = this;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(66.5f);
        timeWait = 0.8f;
        numberOfShootAttacks = 8;
        speedFollow = 13;
        timeShootInOneTime = 0.5f;
        curveHeight = 3.5f;
        speedArc = 1.1f;
        speedToStartPosition = 7f;
        numberOfShootAttacksShockWave = 4;
        speedFollowShockWave = 14f;
        bossNautolanBomShoot.SetFiringRate(0.09f);
    }
    

    private void OnEnable() => healthBar?.SetActive(true);

    public void SetDeadAnimation()
    {
        this.bossNautolanModelShipAnimation.SetIsDestructionTrigger();
        Debug.Log("set destruction");
        this.nautolanAnimator.SetTrigger(IS_DESTRUCTION);
    }

    public void SetIsFollowAndShoot(bool isTrue) => this.nautolanAnimator.SetBool(IS_FOLLOW_AND_SHOOT, isTrue);
    public void SetIsArcShoot(bool isTrue) => this.nautolanAnimator.SetBool(IS_ARC_SHOOT, isTrue);
    public void SetIsBomDrop(bool isTrue) => this.nautolanAnimator.SetBool(IS_BOM_DROP, isTrue);
    public void SetIsShockWave(bool isTrue) => this.nautolanAnimator.SetBool(IS_SHOCK_WAVE, isTrue);
    public Vector3 GetDefaultPosition() => this.defaultPosition;
    public int GetNumberOfAttacksFollowAndShoot() => this.numberOfShootAttacks;
    public float GetSpeedFollow() => this.speedFollow;
    public float GetTimeShootOneTime() => this.timeShootInOneTime;
    public float GetTimeWaitIdle() => this.timeWait;
    public Vector3 GetStartPosition() => startPoint.position;
    public Vector3 GetEndPosition() => endPoint.position;
    public float GetSpeedFollowShockWave() => this.speedFollowShockWave;
    public float GetTimeShootOneTimeShockWave() => this.timeShootInOneTimeShockWave;
    public int GetNumberOfAttacksShockWave() => this.numberOfShootAttacksShockWave;
    public float GetCurveHeight() => this.curveHeight;
    public float GetSpeedArc() => this.speedArc;
    public float GetSpeedToStartPosition() => this.speedToStartPosition;
}
