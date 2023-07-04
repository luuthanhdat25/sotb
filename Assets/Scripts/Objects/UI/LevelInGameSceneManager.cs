using System;
using UnityEngine;

namespace DefaultNamespace.Objects.UI
{
    public class LevelInGameSceneManager : RepeatSceneManager
    {
        [SerializeField] private BackgroundScroller backgroundScroller;
        [SerializeField] private float timeFadeIn = 3f;
        
        [SerializeField] private bool isPause = false;
        
        private void Start() => backgroundScroller?.FadeInBackground(timeFadeIn);
    }
}