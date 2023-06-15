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
            this.LoadPlayerDamageSender();
        }

        private void LoadPlayerMovement()
        {
            if (this.playerMovement != null) return;
            this.playerMovement = GetComponentInChildren<PlayerMovement>();
        }
        
        private void LoadPlayerShoot()
        {
            if (this.playerShoot != null) return;
            this.playerShoot = GetComponentInChildren<PlayerShoot>();
        }
        
        private void LoadPlayerAnimation()
        {
            if (this.playerAnimations != null) return;
            this.playerAnimations = GetComponentInChildren<PlayerAnimations>();
        }
        
        private void LoadPlayerEnergies()
        {
            if (this.playerEnergies != null) return;
            this.playerEnergies = GetComponentInChildren<PlayerEnergies>();
        }
        
        private void LoadPlayerBoostCeils()
        {
            if (this.playerBootCeils != null) return;
            this.playerBootCeils = GetComponentInChildren<PlayerBoostCeils>();
        }

        private void LoadPlayerUnti()
        {
            if (this.playerUnti != null) return;
            this.playerUnti = GetComponentInChildren<PlayerUnti>();
        }
        
        private void LoadPlayerDamageSender()
        {
            if (this.playerDamageSender != null) return;
            this.playerDamageSender = GetComponentInChildren<PlayerDamageSender>();
        }
        
        private void LoadPlayerDamageReciever()
        {
            if (this.playerDamageReciever != null) return;
            this.playerDamageReciever = GetComponentInChildren<PlayerDamageReciever>();
        }
        
        private void LoadPlayerParticle() 
            => this.playerParticleEffect ??= GetComponentInChildren<PlayerParticleEffect>();

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