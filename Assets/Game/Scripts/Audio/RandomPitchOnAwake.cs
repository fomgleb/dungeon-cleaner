using UnityEngine;

namespace Game.Scripts.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class RandomPitchOnAwake : MonoBehaviour
    {
        [SerializeField] private float minPitch;
        [SerializeField] private float maxPitch;

        private AudioSource audioSource;
        
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            audioSource.pitch = Random.Range(minPitch, maxPitch);
        }
    }
}
