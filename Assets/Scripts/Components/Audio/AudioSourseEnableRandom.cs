using UnityEngine;

public class AudioSourseEnableRandom : MonoBehaviour
{
    private AudioSource audioSource;
    [Header("Volume")]
    [SerializeField] private float minVolume = 0.1f;
    [Range(0.2f, 1f)]
    [SerializeField] private float maxVolume;
    [Header("Pitch")]
    [SerializeField] private float minPitch = 0.1f;
    [SerializeField] private float maxPitch = 1.0f;
    
    private void Awake() => audioSource ??= GetComponent<AudioSource>();
    
    private void OnEnable()
    {
        audioSource.volume = Random.Range(minVolume, maxVolume);
        audioSource.pitch = Random.Range(minPitch, maxPitch);
    }
}
