using Player;
using UnityEngine;

namespace UI
{
    public class EnergiesBar : ResourcePlayerBar
    {
        [SerializeField] private int currentEnergies;
        
        protected override void UpdateBarStatus()
        {
            if (PlayerCtrl.Instance == null) return;
            currentEnergies = PlayerCtrl.Instance.PlayerEnergies.GetCurrentEnergies();
            
            for (int index = 0; index < images.Count; index++)
            {
                if (index < this.currentEnergies) images[index].gameObject.SetActive(true);
                else images[index].gameObject.SetActive(false);
            }
        }
    }
}