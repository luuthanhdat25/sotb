using System;
using System.Collections;
using Enemy;
using Enemy.Boss;
using UnityEngine;

public class MiniBossKla_edCtrl : RepeatMonoBehaviour
{
    public static MiniBossKla_edCtrl Instance { get; private set; }
    [SerializeField] private Vector3 defaultPosition = new Vector3(0f, 5f, 0f);
    [SerializeField] protected Transform mainCam;
    [SerializeField] private Animator kla_edAnimator;
    [SerializeField] private MinibossKla_ed_Animation minibossKla_ed_Animation;
    [SerializeField] private EnemyBossDamageReciever enemyBossDamageReciever;
    public EnemyBossDamageReciever EnemyBossDamageReciever { get => enemyBossDamageReciever; }
    
    [SerializeField] private MinibossKla_ed_NormalShoot minibossKla_ed_NormalShoot;
    public MinibossKla_ed_NormalShoot MinibossKla_ed_NormalShoot { get => minibossKla_ed_NormalShoot; }
    
    private const string IS_DESTRUCTION = "isDestruction";
    private const string IS_FOLLOW_AND_SHOOT = "isFollowAndShoot";
    private const string IS_CHASE_PLAYER = "isChasePlayer";
    
    [Header("IdleBehaviour")]
    [SerializeField] private float timeWait = 2f;

    [Header("ShootBehaviour")]
    [SerializeField] private int numberOfShootAttacks = 3;
    [SerializeField] private float speedFollow = 5f;
    [SerializeField] private float timeShootInOneTime = 2f;
    
    [Header("ChaseBehaviour")]
    [SerializeField] private int numberOfChaseAttacks = 2;
    [SerializeField] private float speedChase = 7f;

    protected override void Awake()
    {
        if(Instance != null) Debug.LogError("There is more than one MiniBossKla_edCtrl instance");
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
        this.minibossKla_ed_Animation.SetIsDestructionTrigger();
        this.kla_edAnimator.SetTrigger(IS_DESTRUCTION);
    }

    public void SetIsFollowAndShoot(bool isTrue) => this.kla_edAnimator.SetBool(IS_FOLLOW_AND_SHOOT, isTrue);
    public void SetIsChasePlayer(bool isTrue) => this.kla_edAnimator.SetBool(IS_CHASE_PLAYER, isTrue);

    public Vector3 GetCameraPosition() => this.mainCam.position;
    public Vector3 GetDefaultPosition() => this.defaultPosition;
    public int GetNumberOfAttacksFollowAndShootBehaviour() => this.numberOfShootAttacks;
    public int GetNumberOfAttacksChaseBehaviour() => this.numberOfChaseAttacks;
    public float GetSpeedChase() => this.speedChase;
    public float GetSpeedFollow() => this.speedFollow;
    public float GetTimeShootOneTime() => this.timeShootInOneTime;
    public float GetTimeWaitIdle() => this.timeWait;
}
