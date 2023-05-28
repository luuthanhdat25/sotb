using Player;
using UnityEngine;

namespace Damage
{
    public class EnergyItem : Item
    {
        [SerializeField] private int addValue = 1;
        
        public override void UseItem()
        {
            base.UseItem();
            PlayerCtrl.Instance.PlayerEnergies.AddEnergies(this.addValue);
            //PlayMusic
            //PlayVFX
            ItemDropSpawner.Instance.Despawn(transform.parent);
        }
    }
}