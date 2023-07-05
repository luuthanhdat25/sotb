using System.Collections.Generic;
using DefaultNamespace.Resources.Item;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyName", menuName = "SO/Enemy")]
public class EnemySO : ScriptableObject
{
    public int hpMax = 4;
    public int scoreGain = 100;
    public List<DropRate> dropList;
}
