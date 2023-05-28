using System;
using DefaultNamespace;
using UnityEngine;

namespace Player.Animation
{
    public class PlayerAnimationBaseShooting : MonoBehaviour
    {
        private const string IS_FIRING = "isFiring";
        private Animator _animator;

        private void Start()
        {
            if (_animator != null) return;
            _animator = GetComponent<Animator>();
        }
        
        public void SetIsFiring(bool isFiring) => this._animator.SetBool(IS_FIRING, isFiring);
    }
}