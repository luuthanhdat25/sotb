using UnityEngine;

namespace Damage.SFX
{
    public class SFXCtrl : RepeatMonoBehaviour
    {
        private static SFXCtrl instance;
        public static SFXCtrl Instance { get => instance; }
        
        protected override void Awake()
        {
            base.Awake();
            if (SFXCtrl.instance != null) Debug.LogError("Only 1 SFXCtrl can exist!");
            SFXCtrl.instance = this;
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            
        }
    }
}