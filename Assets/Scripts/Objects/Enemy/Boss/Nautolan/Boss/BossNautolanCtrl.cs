using Enemy.Nautolan;
using UnityEngine;

public class BossNautolanCtrl : RepeatMonoBehaviour
{
    public static BossNautolanCtrl Instance { get; private set; }
    [SerializeField] private Vector3 defaultPosition = new Vector3(0f, 5f, 0f);
    [Header("Component")]
    [SerializeField] private Animator nautolanAnimator;
    [SerializeField] private BossNautolanModelShipAnimation bossNautolanModelShipAnimation;
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
}
