using System.Collections;
using Objects.UI.HUD;
using UnityEngine;

namespace DefaultNamespace.Objects.UI.Level_1
{
    public class LevelOneSceneManager : LevelInGameSceneManager
    {
        public override void WinGame()
        {
            isPause = true;
            UIManager.Instance.WinLevelUI();
        }
        
        public void NextScene() => StartCoroutine(FadeOutSceneCoroutine());

        private IEnumerator FadeOutSceneCoroutine()
        {
            Time.timeScale = 1;
            UIManager.Instance?.FadeOutUI();
            backgroundScroller?.FadeOutBackground(this.timeFadeOut);
            yield return new WaitForSeconds(this.timeFadeOut);
            this.NextSceneIndex();
        }
    }
}