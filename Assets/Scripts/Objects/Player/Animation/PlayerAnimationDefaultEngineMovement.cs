using DefaultNamespace;
using UnityEngine;

namespace Player.Animation
{
    public class PlayerAnimationDefaultEngineMovement : MonoBehaviour
    {
        private const string RAW_INPUT = "rawInput";
        private Animator _animator;

        private void Start()
        {
            if (_animator != null) return;
            _animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            this.SetRawInput();
        }
        
        private void SetRawInput() => this._animator.SetFloat(RAW_INPUT, GameInput.Instance.GetRawInputNormalized().sqrMagnitude);
    }
}