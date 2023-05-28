using DefaultNamespace;
using Player;

public class PlayerDead : RepeatMonoBehaviour
{
    private void FixedUpdate()
    {
        if (PlayerCtrl.Instance.PlayerEnergies.GetCurrentEnergies() <= 0)
        {
            PlayerCtrl.Instance.SetPlayerDead(true);
            PlayerCtrl.Instance.PlayerDamageSender.TurnOffCollider();
            GameManager.Instance.GameOver();
        }
    }
}
