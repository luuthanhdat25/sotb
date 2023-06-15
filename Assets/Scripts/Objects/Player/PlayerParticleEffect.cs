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
        
        public void DashEffect(float dashDuration)
        {
            if(dashEffect == null) return;
            ParticleSystem instance = Instantiate(dashEffect);
            Vector3 directionSpawn = GetOppositeVector(GameInput.Instance.GetRawInputNormalized());
            instance.transform.LookAt(directionSpawn);
            instance.transform.position = transform.parent.position;
            instance.transform.parent = transform.parent;
            Debug.Log(directionSpawn);
            Destroy(instance.gameObject, dashDuration);
        }
        
        

        public Vector2 GetOppositeVector(Vector2 vector) => new Vector2(-vector.x, -vector.y);
    }
}