using System;
using System.Collections.Generic;
using Damage.RhythmScripts;
using Enemy;
using UnityEngine;

public class Pathfinder : RepeatMonoBehaviour
{
    [SerializeField] private Transform behaviour;
    private WaveSpawner waveSpawner;
    private int wayPointIndex = 0;
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWaveSpawner();
        this.LoadBehaviourTransform();
    }

    protected virtual void LoadWaveSpawner()
    {
        if (this.waveSpawner != null) return;
        this.waveSpawner = transform.parent.parent.GetComponent<WaveSpawner>();
    }

    protected virtual void LoadBehaviourTransform()
    {
        if (this.behaviour != null) return;
        this.behaviour = transform.Find("Behaviour");
        if (this.behaviour == null) return;
        this.behaviour.gameObject.SetActive(false);
    }

    protected virtual void Start() => StartingMove();

    protected virtual void StartingMove()
    {
        if(waveSpawner == null) return;
        transform.position = waveSpawner.GetStartingWaypoint().position;
    }
    //------------------------------------------------------------------------//
    protected virtual void FixedUpdate() => FollowPath();
    
    protected virtual void FollowPath()
    {
        if(!IsFinishPath())
        {
            var nextWavePointPosition = GetNextPointPosition();
            this.MoveToNextPoint(nextWavePointPosition);
        }
        else FinishPath();
    }
    
    protected virtual bool IsFinishPath() => wayPointIndex >= waveSpawner.pathWayPoints.Count;
    
    public virtual Vector3 GetNextPointPosition()
    {
        if(wayPointIndex >= waveSpawner.pathWayPoints.Count) return waveSpawner.pathWayPoints[waveSpawner.pathWayPoints.Count - 1].position;
        return waveSpawner.pathWayPoints[wayPointIndex].position;
    }

    protected virtual void MoveToNextPoint(Vector3 nextWavePointPosition)
    {
        float realSpeed = waveSpawner.GetMoveSpeed() * Time.fixedDeltaTime;
        transform.position = Vector2.MoveTowards(transform.position, nextWavePointPosition, realSpeed);
        
        if(transform.position == nextWavePointPosition) wayPointIndex++;
    }

    protected virtual void FinishPath()
    {
        if(this.behaviour == null) return;
        this.behaviour.gameObject.SetActive(true);
    }

    public WaveSpawner GetWaveSpawner() => this.waveSpawner;
}
