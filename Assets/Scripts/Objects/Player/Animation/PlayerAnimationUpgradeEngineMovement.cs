using DefaultNamespace;
using UnityEngine;

namespace Player.Animation
{
    public class PlayerAnimationUpgradeEngineMovement : MonoBehaviour
    {
        private const string RAW_INPUT = "rawInput";
        private const string IS_DASH = "isDash";

        private Animator _animator;

        private void Start()
        {
            if (_animator != null) return;
            _animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            this.SetRawInput();
            this.SetIsDash();
        }
        
        private void SetRawInput() => this._animator.SetFloat(RAW_INPUT, GameInput.Instance.GetRawInputNormalized().sqrMagnitude);
        private void SetIsDash() => this._animator.SetBool(IS_DASH, GameInput.Instance.IsDashPressed());

    }
}