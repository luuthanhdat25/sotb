using DefaultNamespace;
using DefaultNamespace.Components.Time;
using Player;
using UnityEngine;

public class PlayerEnergies : MonoBehaviour
{
    [SerializeField] private int startEnergies;
    [SerializeField] private int currentEnergies;
    
    private void Start() => this.currentEnergies = startEnergies;
    
    public void DeductEnergies(int energiesDeduct)
    {
        currentEnergies -= energiesDeduct;
        this.CheckIsGameOver();
    }

    private void CheckIsGameOver()
    {
        if (currentEnergies > 0) return;
        PlayerCtrl.Instance.SetPlayerDead(true);
        PlayerCtrl.Instance.PlayerDamageReciever.SetActiveCollider(false);
        TimeManager.Instance?.SlowMotionEffect();
        GameManager.Instance.GameOver();
    }
    
    public bool IsEnoughEnergies(int energiesDeduct) 
        => currentEnergies >= energiesDeduct;
    
    public void AddEnergies(int addValue) 
        => currentEnergies = Mathf.Min(currentEnergies + addValue, PlayerCtrl.Instance.GetEnergiesDefault());

    public int GetCurrentEnergies() => this.currentEnergies;
    public bool IsMaxHealth() => this.currentEnergies == startEnergies;
}
