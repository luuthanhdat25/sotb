using System;
using UnityEngine;

public class CameraAnimation : RepeatMonoBehaviour
{
    public static CameraAnimation Instance { get; private set; }

    [SerializeField] private Animator animator;
    private const string SHAKE_TRIGGER = "shakeTrigger";
    
    protected override void Awake()
    {
        base.Awake();
        if(Instance != null) Debug.LogError("There is more than one CameraAnimation instance");
        Instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCamera();
    }

    private void LoadCamera()
    {
        if (this.animator != null) return;
        this.animator = GetComponent<Animator>();
    }

    public void ShakeCamera()
    {
        this.animator.SetTrigger(SHAKE_TRIGGER);
    }
    
    
}
