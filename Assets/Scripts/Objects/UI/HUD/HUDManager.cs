using DefaultNamespace;
using TMPro;
using UnityEngine;

namespace Objects.UI.HUD
{
    public class HUDManager : RepeatMonoBehaviour
    {
        public static HUDManager Instance { get; private set; }

        [SerializeField] private Transform energiesBar;
        [SerializeField] private Transform boostceilBar;
        [SerializeField] private Transform winUI;
        [SerializeField] private Transform lossUI;
        [SerializeField] private TMP_Text scoreWinText;
        [SerializeField] private TMP_Text scoreLossText;
        [SerializeField] private TMP_Text scoreText;
        protected override void Awake()
        {
            if(Instance != null) Debug.LogError("There is more than one MiniBossKla_edCtrl instance");
            Instance = this;
        }

        private void Start()
        {
            GameManager.Instance.onScoreChanged.AddListener(UpdateScore);
        }

        public void SetActiveEnergiesBar(bool isTrue) => energiesBar.gameObject.SetActive(isTrue);
        public void SetActiveBoostceilBar(bool isTrue) => boostceilBar.gameObject.SetActive(isTrue);
        public void SetActiveWinUI(bool isTrue) => winUI.gameObject.SetActive(isTrue);
        public void SetActiveLossUI(bool isTrue) => lossUI.gameObject.SetActive(isTrue);
        public void UpdateScoreLossUI(int score) => scoreLossText.text = score + "";
        public void UpdateScoreWinUI(int score) => scoreWinText.text = score + "";
        
        public void UpdateScore()
        {
            if (scoreText == null) return;
            int score = GameManager.Instance.GetScore();
            if(score < 999) this.scoreText.text = "000" + score + "";
            else if(score < 9999)
            {
                this.scoreText.text = "00" + score + "";
            }else if(score < 99990)
            {
                this.scoreText.text = "0" + score + "";
            }else this.scoreText.text = score + "";
        }
    }
}