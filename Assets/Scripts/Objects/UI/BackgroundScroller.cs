using System;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private float scrollSpeedDefault = 0.3f;
    [SerializeField] private float scrollDashSpeed = 0.6f;
    private bool isDash = false;

    private void Awake()
    {
        if(this.meshRenderer != null) return;
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void FixedUpdate()
    {
        if(!isDash) Scroll(scrollSpeedDefault);
        else Scroll(scrollDashSpeed);
    }

    public void Dash(bool isOn) => this.isDash = isOn;

    private void Scroll(float speed)
    {
        float delta = speed / transform.localScale.x;
        meshRenderer.material.mainTextureOffset += Vector2.up * delta * Time.fixedDeltaTime;
    }
}
