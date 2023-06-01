using System;
using UnityEngine;

namespace Despawn
{
    public class DespawnByTime : Despawn
    {
        [SerializeField] private float timeDelay = 2;
        [SerializeField] private float timer = 0;

        protected virtual void OnEnable() => this.ResetTimer();

        protected virtual void ResetTimer() => this.timer = 0;
        
        protected override bool CanDespawn()
        {
            timer += Time.fixedDeltaTime;
            if (timer >= timeDelay) return true;
            return false;
        }

        protected override void DespawnObject()
        {
            //For override
        }
    }
}