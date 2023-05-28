using System;
using DefaultNamespace;
using Player;
using UnityEngine;

public class PlayerBoostCeils : MonoBehaviour
{
    [SerializeField] private float timeRecoveryCeils = 2;
    [SerializeField] private float recoverySpeedIncreasePercent = 0.2f;
    [SerializeField] private float timeDelayZeroRecovery = 4;
    [SerializeField] private int boostCeilValue = 1;
    
    private int currentBoostCeils;
    private int defaultBoostCeils;
    private float defaultTimeRecoveryCeils;
    private float timer = 0;
    
    private void Start()
    {
        SetDefaultValue();
    }

    private void SetDefaultValue()
    {
        this.defaultBoostCeils = PlayerCtrl.Instance.GetBoostCeilsDefault();
        this.currentBoostCeils = defaultBoostCeils;
        
        this.defaultTimeRecoveryCeils = timeRecoveryCeils;
    }

    private void FixedUpdate()
    {
        this.HandleRecoveryCeils();
    }
    
    private void HandleRecoveryCeils()
    {
        if (currentBoostCeils == 0 && !PlayerCtrl.Instance.PlayerMovement.IsDash())
        {
            RecoverCeilsByTimer(boostCeilValue, timeDelayZeroRecovery);
        }
        else if (currentBoostCeils < defaultBoostCeils)
        {
            RecoverCeilsByTimer(boostCeilValue, timeRecoveryCeils);
            DecreaseTimeRecoveryCeilsOverTime();
        }
    }
    
    private void RecoverCeilsByTimer(int ceilAdd, float timeGoal)
    {
        this.timer += Time.fixedDeltaTime;
        if (this.timer >= timeGoal)
        {
            AddBoostCeils(ceilAdd);
            ResetTimer();
        }
    }
    
    private void DecreaseTimeRecoveryCeilsOverTime()
    {
        timeRecoveryCeils -= timeRecoveryCeils * recoverySpeedIncreasePercent * Time.fixedDeltaTime;
    }
    
    public void AddBoostCeils(int addValue)
    {
        currentBoostCeils = Mathf.Min(currentBoostCeils + addValue, PlayerCtrl.Instance.GetBoostCeilsDefault());
    }
    
    private void ResetTimer() => timer = 0;
    
    public void DeductCeilsByValue(int value)
    {
        if (!CanDeductByValue(value)) return;
        this.ResetTimeRecoveryCeils();
        currentBoostCeils -= value;
    }
    
    public bool CanDeductByValue(int value) => currentBoostCeils >= value;
    private void ResetTimeRecoveryCeils() => timeRecoveryCeils = defaultTimeRecoveryCeils;
    public int GetCurrentBootCeils() => this.currentBoostCeils;
}
