using Objects.Enemy.AttackEnemy;
using UnityEngine;

public class DropBom : MonoBehaviour
{
    public void Drop()
    {
        Transform itemDrop = EnemyProjectileSpawner.Instance.Spawn(EnemyProjectileSpawner.Instance.bom);
        if (itemDrop == null) return;
        itemDrop.position = transform.parent.position;
        itemDrop.gameObject.SetActive(true);
    }
}
