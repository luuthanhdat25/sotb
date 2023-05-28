using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemy.Troubled_Enemy
{
    public class TroubledEnemyBehaviour : MonoBehaviour
    {
        private void Start()
        {
            Destroy(transform.parent.gameObject);
        }
    }
}