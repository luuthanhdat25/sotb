using UnityEngine;

namespace DefaultNamespace
{
    public class GameInput : RepeatMonoBehaviour
    {
        public static GameInput Instance { get; private set; }

        [SerializeField] private PlayerInputActions playerInputActions;

        protected override void Awake()
        {
            this.EnableInputAction();
            this.CreateInstance();
        }

        private void EnableInputAction()
        {
            playerInputActions = new PlayerInputActions();
            playerInputActions.Enable();
        }

        private void CreateInstance()
        {
            if (Instance != null) Debug.LogError("There is more than one GameInput instance");
            Instance = this;
        }
        //---------------------------------------------------------------------------//
        public Vector2 GetRawInputNormalized()
        {
            Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
            inputVector = inputVector.normalized;
            return inputVector;
        }
        
        public bool IsFirePressed()    => playerInputActions.Player.Fire.IsPressed();
        public bool IsDashPressed() => playerInputActions.Player.Speed.IsPressed();
        public bool IsUntiPressed()    => playerInputActions.Player.Unti.IsPressed();
       
    }
}