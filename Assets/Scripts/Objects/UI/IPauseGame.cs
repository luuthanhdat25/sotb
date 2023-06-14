using UnityEngine;

namespace DefaultNamespace.Objects.UI
{
    public interface IPauseGame
    {
        [SerializeField] bool IsPause { get; set; }
        [SerializeField] Transform PauseUI { get; set; }
        [SerializeField] Transform PauseUIContent { get; set; }
        void CheckPauseOrContinue();
        void PauseGame();
        void ContinueGame();
    }
}