using System.Collections;
using UnityEngine;

public class MinibossNautolanCtrl : RepeatMonoBehaviour
{
    public static MinibossNautolanCtrl Instance { get; private set; }
    [SerializeField] private Vector3 defaultPosition = new Vector3(0f, 5f, 0f);
    [SerializeField] protected Transform mainCam;
    [SerializeField] private Animator miniNautolanAnimator;
    [SerializeField] private MinibossNautolanModelShipAnimation minibossNautolanModelShipAnimation;
    public MinibossNautolanModelShipAnimation MinibossNautolanModelShipAnimation => minibossNautolanModelShipAnimation;
    [SerializeField] private MinibossNautolanShoot minibossNautolanShoot;

    public MinibossNautolanShoot MinibossNautolanShoot { get => minibossNautolanShoot; }

    private const string IS_DESTRUCTION = "isDestruction";
    private const string IS_FOLLOW_AND_SHOOT = "isFollowAndShoot";
    private const string IS_CHASE_PLAYER = "isChasePlayer";
    
    [Header("IdleBehaviour")]
    [SerializeField] private float timeWait = 2f;

    [Header("ShootBehaviour")]
    [SerializeField] private int numberOfShootAttacks = 1;
    [SerializeField] private float speedFollow = 6f;
    [SerializeField] private float timeShootInOneTime = 5f;
    
    [Header("ChaseBehaviour")]
    [SerializeField] private int numberOfChaseAttacks = 2;
    [SerializeField] private float speedChase = 7f;

    protected override void Awake()
    {
        if(Instance != null) Debug.LogError("There is more than one MinibossNautolanCtrl instance");
        Instance = this;
    }

    private void Start()
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
    }

    public void SetDeadAnimation()
    {
        this.minibossNautolanModelShipAnimation.SetIsDestructionTrigger();
        Debug.Log("set destruction");
        this.miniNautolanAnimator.SetTrigger(IS_DESTRUCTION);
    }

    public void SetIsFollowAndShoot(bool isTrue) => this.miniNautolanAnimator.SetBool(IS_FOLLOW_AND_SHOOT, isTrue);
    public void SetIsChasePlayer(bool isTrue) => this.miniNautolanAnimator.SetBool(IS_CHASE_PLAYER, isTrue);
    public Vector3 GetCameraPosition() => this.mainCam.position;
    public Vector3 GetDefaultPosition() => this.defaultPosition;
    public int GetNumberOfAttacksFollowAndShootBehaviour() => this.numberOfShootAttacks;
    public int GetNumberOfAttacksChaseBehaviour() => this.numberOfChaseAttacks;
    public float GetSpeedChase() => this.speedChase;
    public float GetSpeedFollow() => this.speedFollow;
    public float GetTimeShootOneTime() => this.timeShootInOneTime;
    public float GetTimeWaitIdle() => this.timeWait;
}
