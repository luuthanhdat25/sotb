using DefaultNamespace;
using UnityEngine;

namespace Player.Animation
{
    public class PlayerAnimationDefaultEngineMovement : MonoBehaviour
    {
        private const string RAW_INPUT = "rawInput";
        private Animator animator;
        
        private void Start() => animator ??= GetComponent<Animator>();

        private void Update() => this.SetRawInput();

        private void SetRawInput() => this.animator.SetFloat(RAW_INPUT, GameInput.Instance.GetRawInputNormalized().sqrMagnitude);
    }
}