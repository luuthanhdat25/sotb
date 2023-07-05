using Objects.UI.HUD;

namespace DefaultNamespace.Objects.UI.Level_3
{
    public class LevelThreeSceneManager : LevelInGameSceneManager
    {
        public override void WinGame()
        {
            isPause = true;
            GameManager.Instance.SetIsStopTimer(true);
            UIManager.Instance.WinGameUI();
        }
    }
}