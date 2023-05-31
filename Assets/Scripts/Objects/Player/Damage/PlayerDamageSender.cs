using System.Collections;
using Damage;
using UnityEngine;

namespace Player
{
    public class PlayerDamageSender : DamageSender
    {
        public override void GotHit()
        {
            base.GotHit();
            CameraAnimation.Instance.ShakeCamera();
        }
    }
}