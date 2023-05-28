using System;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private float scrollSpeed = 0.3f;
    

    private void Awake()
    {
        if(this.meshRenderer != null) return;
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void FixedUpdate()
    {
        Scroll();
    }

    private void Scroll()
    {
        float speed = scrollSpeed / transform.localScale.x;
        meshRenderer.material.mainTextureOffset += Vector2.up * speed * Time.fixedDeltaTime;
    }
}
