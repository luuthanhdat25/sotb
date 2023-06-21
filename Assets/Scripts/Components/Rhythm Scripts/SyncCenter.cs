using UnityEngine;

namespace Damage.RhythmScripts
{
    public class SyncCenter : MonoBehaviour
    {
        /*
         * Cái này hợp để sử lý ánh sáng cùng nhạc hơn là spawn
         */
        
        public AudioSource audioSource;

        public delegate void listen();
        public event listen Sync;

        public float _emissionMultiplier;
        public float _spectrumThreshold; 
        
        void Start()
        {
            // Get the audio source component attached to this game object
            audioSource = GetComponent<AudioSource>();

            // Start playing the music clip
            audioSource.Play();
        }
        
        void Update()
        {
            float[] spectrum = new float[1024];

            if (audioSource == null)
                AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
            else
                audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

            //Do Event
            if (SpectrumAverage(spectrum) * _emissionMultiplier > _spectrumThreshold && Sync != null)
                Sync();

        }

        private float SpectrumAverage(float[] spectrumBlock)
        {
            float average = 0;
            float sum = 0;

            for (int i = 1; i < spectrumBlock.Length; i++)
            {
                sum += spectrumBlock[i];
            }

            average = sum / spectrumBlock.Length;
            return average;
        }
    }
}