using Damage;

namespace Player
{
    public class PlayerDamageSender : DamageSender
    {
        public override void GotHit()
        {
            CameraAnimation.Instance.ShakeCamera();
        }
    }
}