using UnityEngine;

namespace DefaultNamespace.Components.Time
{
    public class TimeManager : MonoBehaviour
    {
        public static TimeManager Instance { get; private set; }
        
        [SerializeField] private float slowMotionDuration = 2f;
        [SerializeField] private float slowlTimeScale = 0.4f;
        [SerializeField] private float normalTimeScale = 1f;
        
        private void Awake()
        {
            if(Instance != null) Debug.LogError("There is more than one TimeManager instance");
            Instance = this;
        }
        
        public void SlowMotionEffect()
        {
            UnityEngine.Time.timeScale = slowlTimeScale;
            Invoke("ResumeNormalTimeScale", slowMotionDuration);
        }

        private void ResumeNormalTimeScale() => UnityEngine.Time.timeScale = normalTimeScale; // Reset the time scale to normal
    }
}