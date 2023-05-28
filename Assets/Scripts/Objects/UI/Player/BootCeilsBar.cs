using System;
using Player;
using UnityEngine;

namespace UI
{
    public class BootCeilsBar : ResourcePlayerBar
    {
        [SerializeField] private int currentBootCeils;
        
        protected override void UpdateBarStatus()
        {
            if (PlayerCtrl.Instance == null) return;
            currentBootCeils = PlayerCtrl.Instance.PlayerBootCeils.GetCurrentBootCeils();
            
            
            for (int index = 0; index < images.Count; index++)
            {
                if (index < this.currentBootCeils) images[index].gameObject.SetActive(true);
                else images[index].gameObject.SetActive(false);
            }
        }
    }
}