using Damage;
using UnityEngine;

namespace Player
{
    public class PlayerCtrl : RepeatMonoBehaviour
    {
        public static PlayerCtrl Instance { get; private set; }

        [Header("Player Default Setting")] 
        [SerializeField] private Transform defaultPosition;
        [SerializeField] private int playerEnergiesMax; 
        [SerializeField] private int playerBoostCeilsMax;
        [SerializeField] private bool isPlayerDead = false;
        [SerializeField] private BackgroundScroller backgroundScroller;
        public BackgroundScroller BackgroundScroller => backgroundScroller;
        [SerializeField] private PlayerMovement playerMovement;
        public PlayerMovement PlayerMovement { get => playerMovement; }
        
        [SerializeField] private PlayerShoot playerShoot;
        public PlayerShoot PlayerShoot { get => playerShoot; }
        
        [SerializeField] private PlayerAnimations playerAnimations;
        public PlayerAnimations PlayerAnimations { get => playerAnimations; }
        
        [SerializeField] private PlayerEnergies playerEnergies;
        public PlayerEnergies PlayerEnergies { get => playerEnergies; }
        
        [SerializeField] private PlayerUnti playerUnti;
        public PlayerUnti PlayerUnti { get => playerUnti; }
        
        [SerializeField] private PlayerBoostCeils playerBootCeils;
        public PlayerBoostCeils PlayerBootCeils { get => playerBootCeils; }
        
        [SerializeField] private PlayerDamageSender playerDamageSender;
        public PlayerDamageSender PlayerDamageSender { get => playerDamageSender; }
        
        [SerializeField] private PlayerDamageReciever playerDamageReciever;
        public PlayerDamageReciever PlayerDamageReciever { get => playerDamageReciever; }
        [SerializeField] private PlayerParticleEffect playerParticleEffect;
        public PlayerParticleEffect PlayerParticleEffect { get => playerParticleEffect; }
        [SerializeField] private ItemMagnet playerItemMagnet;
        public ItemMagnet ItemMagnet { get => playerItemMagnet; }
        
        protected override void Awake()
        {
            if(Instance != null) Debug.LogError("There is more than one PlayerCtrl instance");
            Instance = this;
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadPlayerMovement();
            this.LoadPlayerShoot();
            this.LoadPlayerAnimation();
            this.LoadPlayerEnergies();
            this.LoadPlayerBoostCeils();
            this.LoadPlayerUnti();
            this.LoadPlayerDamageReciever();
            this.LoadPlayerDamageSender();
            this.LoadPlayerItemMagnet();
            this.LoadPlayerParticle();
        }

        private void LoadPlayerMovement() => this.playerMovement ??= GetComponentInChildren<PlayerMovement>();

        private void LoadPlayerShoot() => this.playerShoot ??= GetComponentInChildren<PlayerShoot>();

        private void LoadPlayerAnimation() => this.playerAnimations ??= GetComponentInChildren<PlayerAnimations>();

        private void LoadPlayerEnergies() => this.playerEnergies ??= GetComponentInChildren<PlayerEnergies>();

        private void LoadPlayerBoostCeils() => this.playerBootCeils ??= GetComponentInChildren<PlayerBoostCeils>();

        private void LoadPlayerUnti() => this.playerUnti ??= GetComponentInChildren<PlayerUnti>();

        private void LoadPlayerDamageSender() => this.playerDamageSender ??= GetComponentInChildren<PlayerDamageSender>();

        private void LoadPlayerDamageReciever() => this.playerDamageReciever ??= GetComponentInChildren<PlayerDamageReciever>();
        
        private void LoadPlayerItemMagnet() => this.playerItemMagnet ??= GetComponentInChildren<ItemMagnet>();

        private void LoadPlayerParticle() => this.playerParticleEffect ??= GetComponentInChildren<PlayerParticleEffect>();

        //-------------------------------------------------------------------------//
        private void Start() => transform.position = this.GetDefaultPosition();
        
        public int GetEnergiesDefault() => this.playerEnergiesMax; 
        public int GetBoostCeilsDefault() => this.playerBoostCeilsMax;
        public Vector3 GetCurrentPosition() => transform.position;
        public Vector2 GetDefaultPosition() => defaultPosition.position;
        public void SetPlayerDead(bool isDead) => this.isPlayerDead = isDead;
        public bool GetIsPlayerDead() => this.isPlayerDead;
    }
}