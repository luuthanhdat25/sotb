using System;
using System.Collections;
using Damage.RhythmScripts;
using DefaultNamespace;
using DefaultNamespace.Components.Time;
using Enemy;
using Enemy.Boss;
using UnityEngine;

public class MiniBossKla_edCtrl : RepeatMonoBehaviour
{
    public static MiniBossKla_edCtrl Instance { get; private set; }
    [SerializeField] private Vector3 defaultPosition = new Vector3(0f, 5f, 0f);
    [SerializeField] protected Transform mainCam;
    [SerializeField] private Animator kla_edAnimator;
    [SerializeField] private MinibossKla_edModelShipAnimation minibossKlaEdModelShipModelShipAnimation;
    public MinibossKla_edModelShipAnimation MinibossKlaEdModelShipAnimation { get => minibossKlaEdModelShipModelShipAnimation; }
    [SerializeField] private MinibossKla_edDamageReciever minibossKlaEdDamageReciever;
    public MinibossKla_edDamageReciever MinibossKlaEdDamageReciever { get => minibossKlaEdDamageReciever; }
    
    [SerializeField] private MinibossKla_ed_NormalShoot minibossKla_ed_NormalShoot;
    public MinibossKla_ed_NormalShoot MinibossKla_ed_NormalShoot { get => minibossKla_ed_NormalShoot; }

    [SerializeField] private GameObject healthBar;
    
    private enum AnimationParameter
    {
        isDestruction,
        isFollowAndShoot,
        isChasePlayer
    }
    
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

    private void OnEnable() => healthBar?.SetActive(true);

    private void Start() => StartCoroutine(AfterOnMinute());

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
        TimeManager.Instance.SlowMotionEffect();
        this.minibossKlaEdModelShipModelShipAnimation.SetIsDestructionTrigger();
        AudioManager.Instance.SpawnEnemyEffect(AudioManager.SoundEffectEnum.ExplosionBoss);
        this.kla_edAnimator.SetTrigger(AnimationParameter.isDestruction.ToString());
    }

    public void SetIsFollowAndShoot(bool isTrue) => this.kla_edAnimator.SetBool(AnimationParameter.isFollowAndShoot.ToString(), isTrue);
    public void SetIsChasePlayer(bool isTrue) => this.kla_edAnimator.SetBool(AnimationParameter.isChasePlayer.ToString(), isTrue);

    public Vector3 GetCameraPosition() => this.mainCam.position;
    
    public Vector3 GetDefaultPosition() => this.defaultPosition;
    
    public int GetNumberOfAttacksFollowAndShootBehaviour() => this.numberOfShootAttacks;
    
    public int GetNumberOfAttacksChaseBehaviour() => this.numberOfChaseAttacks;
    
    public float GetSpeedChase() => this.speedChase;
    
    public float GetSpeedFollow() => this.speedFollow;
    
    public float GetTimeShootOneTime() => this.timeShootInOneTime;
    
    public float GetTimeWaitIdle() => this.timeWait;
}
