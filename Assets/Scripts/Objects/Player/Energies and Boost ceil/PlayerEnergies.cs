using Player;
using UnityEngine;

public class PlayerEnergies : MonoBehaviour
{
    [SerializeField] private int startEnergies;
    [SerializeField] private int currentEnergies;
    
    private void Start() => this.currentEnergies = startEnergies;
    
    public void DeductEnergies(int energiesDeduct) => currentEnergies -= energiesDeduct;
    
    public bool IsEnoughEnergies(int energiesDeduct) => currentEnergies > energiesDeduct;
    
    public void AddEnergies(int addValue)
    {
        currentEnergies = Mathf.Min(currentEnergies + addValue, PlayerCtrl.Instance.GetEnergiesDefault());
    }
    
    public int GetCurrentEnergies() => this.currentEnergies;
}
