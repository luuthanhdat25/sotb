using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class BackgroundScroller : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    
    [SerializeField] private float scrollSpeedDefault = 0.3f;
    [SerializeField] private float rateDashSpeed = 1.2f;
    
    [Header("Fade Out Scene")]
    [SerializeField] private float speedLight = 4f;
    
    private bool isDash = false;
    private bool isReverseBackground = false;
    private float currentSpeed;
    private bool isInSceneTransitions = false; 
    
    private void Start() => meshRenderer = GetComponent<MeshRenderer>();

    private void FixedUpdate()
    {
        if (!isInSceneTransitions)
        {
            if (!isDash)
            {
                currentSpeed = scrollSpeedDefault;
            }
            else
            {
                currentSpeed = scrollSpeedDefault * rateDashSpeed;
            } 
        }
        Scroll(currentSpeed);
    }

    public void Dash(bool isOn) => this.isDash = isOn;

    private void Scroll(float speed)
    {
        float delta = speed / transform.localScale.x;
        meshRenderer.material.mainTextureOffset += GetVectorMove() * delta * Time.fixedDeltaTime;
    }

    private Vector2 GetVectorMove()
    {
        if(!isReverseBackground) return Vector2.up;
        return Vector2.down;
    }
    
    public void ReverseBackground() => isReverseBackground = true;

    public void FadeOutBackground(float timeFadeOut)
    {
        isInSceneTransitions = true;
        StartCoroutine(IncreaseSpeed(timeFadeOut));   
    }
    
    private IEnumerator IncreaseSpeed(float timeFadeOut) 
    {
        float elapsedTime = 0;
        float timeIncreaseSpeed = timeFadeOut;
        while (elapsedTime < timeIncreaseSpeed)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, speedLight, elapsedTime / timeIncreaseSpeed);
            elapsedTime += UnityEngine.Time.deltaTime;
            yield return null;
        }
        currentSpeed = speedLight;
    }
    
    public void FadeInBackground(float timeFadeIn)
    {
        isInSceneTransitions = true;
        StartCoroutine(DecreaseSpeed(timeFadeIn));   
    }
    
    private IEnumerator DecreaseSpeed(float timeFadeIn) 
    {
        float elapsedTime = 0;
        float timeIncreaseSpeed = timeFadeIn;
        while (elapsedTime < timeIncreaseSpeed)
        {
            currentSpeed = Mathf.Lerp(speedLight, scrollSpeedDefault, elapsedTime / timeIncreaseSpeed);
            elapsedTime += UnityEngine.Time.deltaTime;
            yield return null;
        }
        currentSpeed = scrollSpeedDefault;
        isInSceneTransitions = false;
        
        GameManager.Instance.SetIsStopTimer(false);
    }
}
