using System;
using System.Collections.Generic;
using Damage.RhythmScripts;
using Enemy;
using UnityEngine;

public class Pathfinder : RepeatMonoBehaviour
{
    public WaveSpawner waveSpawner { get; private set; }
    protected int wayPointIndex = 0;
    [SerializeField] protected Transform behaviour;
    
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

    protected virtual void Start()
    {
        StartingMove();
    }

    protected virtual void StartingMove()
    {
        if(waveSpawner == null) return;
        transform.position = waveSpawner.GetStartingWaypoint().position;
    }
    //------------------------------------------------------------------------//
    protected virtual void FixedUpdate()
    {
        FollowPath();
    }

    protected virtual void FollowPath()
    {
        if(!IsFinishPath())
        {
            var nextWavePointPosition = GetNextPointPosition();
            this.MoveToNextPoint(nextWavePointPosition);
        }
        else
        {
            FinishPath();
        }
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
        //For Override
        if(this.behaviour == null) return;
        this.behaviour.gameObject.SetActive(true);
    }
}
