using DefaultNamespace;
using UnityEngine;

namespace Player
{
    public class PlayerParticleEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem dashEffect;
        [SerializeField] private ParticleSystem healthEffect;
        [SerializeField] private ParticleSystem buffMoveSpeedEffect;
        [SerializeField] private ParticleSystem buffShootSpeedEffect;
        [SerializeField] private ParticleSystem untiShootEffect;
        
        public void DashEffect(float dashDuration)
        {
            if(dashEffect == null) return;
            ParticleSystem instance = Instantiate(dashEffect);
            Vector3 directionSpawn = GetOppositeVector(GameInput.Instance.GetRawInputNormalized());
            instance.transform.LookAt(directionSpawn);
            instance.transform.position = transform.parent.position;
            instance.transform.parent = transform.parent;
            Destroy(instance.gameObject, dashDuration);
        }
        public Vector2 GetOppositeVector(Vector2 vector) => new Vector2(-vector.x, -vector.y);
        
        public void HealthEffect()
        {
            if(PlayerCtrl.Instance.PlayerEnergies.IsMaxHealth()) return;
            ItemEffect(healthEffect);
        }

        public void BuffMoveSpeedEffect() => ItemEffect(buffMoveSpeedEffect);
        
        public void BuffShootSpeedEffect() => ItemEffect(buffShootSpeedEffect);

        private void ItemEffect(ParticleSystem particle)
        {
            if(particle == null) return;
            ParticleSystem instance = Instantiate(particle);
            Vector3 directionSpawn = Vector3.up;
            instance.transform.LookAt(directionSpawn);
            instance.transform.position = transform.parent.position;
            instance.transform.parent = transform.parent;
            Destroy(instance.gameObject, instance.main.duration);
        }

        public void UntiShootEffect()
        {
            if(untiShootEffect == null) return;
            ParticleSystem instance = Instantiate(untiShootEffect);
            Vector3 directionSpawn = Vector3.down;
            instance.transform.LookAt(directionSpawn);
            instance.transform.position = transform.parent.position;
            Destroy(instance.gameObject, instance.main.duration);
        }
    }
}