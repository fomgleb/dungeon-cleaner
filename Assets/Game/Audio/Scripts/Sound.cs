using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Audio.Scripts
{
    [Serializable]
    public struct Sound
    {
        [SerializeField] private AudioClip[] randomAudioClips;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private float minRandomPitch;
        [SerializeField] private float maxRandomPitch;
        public AudioClip[] RandomAudioClips => randomAudioClips;
        public AudioSource AudioSource => audioSource;
        public float MinRandomPitch => minRandomPitch;
        public float MaxRandomPitch => maxRandomPitch;

        public void Play()
        {
            audioSource.pitch = Random.Range(minRandomPitch, maxRandomPitch);
            audioSource.PlayOneShot(randomAudioClips[Random.Range(0, randomAudioClips.Length)]);
        }
    }
}
