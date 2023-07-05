using UnityEngine;

namespace DefaultNamespace
{
    /// <summary>
    /// This class use for save data on web pages
    /// </summary>
    public class DataManager : MonoBehaviour
    {
        public static DataManager Instance { get; private set; }
        
        [SerializeField] private int score = 0;
        [SerializeField] private float timeFinished = 0;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SaveData(int score, float timeFinished)
        {
            this.score = score;
            this.timeFinished = timeFinished;
        }
        
        public int GetScore() => this.score;
        
        public float GetTimeFinished() => this.timeFinished;
    }
}