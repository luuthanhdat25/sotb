using System;
using DefaultNamespace;
using UnityEngine;

namespace Player.Animation
{
    public class PlayerAnimationUntiShooting : MonoBehaviour
    {
        private const string IS_UNTI = "isUnti";
        private Animator _animator;

        private void Start()
        {
            if (_animator != null) return;
            _animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            this.SetIsUnti();
        }


        public void SetIsUnti()
        {
            if (!PlayerCtrl.Instance.PlayerUnti.GetIsFire()) return;
            this._animator.SetBool(IS_UNTI, GameInput.Instance.IsUntiPressed());
        }
    }
}