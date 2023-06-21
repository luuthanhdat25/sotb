using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScoreUIRaise : MonoBehaviour
{
    private AudioSource audioSource;
    [Header("Volume")]
    [SerializeField] private float minVolume = 0.1f;
    [Range(0.5f, 1.0f)]
    [SerializeField] private float maxVolume;
    [Header("Pitch")]
    [SerializeField] private float minPitch = 0.5f;
    [Range(0.8f, 3.0f)]
    [SerializeField] private float maxPitch;
    
    private void Awake() => audioSource ??= GetComponent<AudioSource>();
    
    private void OnEnable()
    {
        audioSource.volume = Random.Range(minVolume, maxVolume);
        audioSource.pitch = Random.Range(minPitch, maxPitch);
    }
}
