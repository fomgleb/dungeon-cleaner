using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SmoothMusicAppearing : MonoBehaviour
{
    [SerializeField] private float startVolume = 0;
    [SerializeField] private float appearingTime = 10;
    [SerializeField] private float timeStep = 0.03f;

    private AudioSource audioSource;
    private float endVolume;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        endVolume = audioSource.volume;
        audioSource.volume = startVolume;
        SmoothAppearAsync();
    }

    private async void SmoothAppearAsync()
    {
        while (audioSource.volume < endVolume)
        {
            var volumeStep = timeStep / timeStep;
            print(volumeStep);
            audioSource.volume += volumeStep;
            await UniTask.Delay(TimeSpan.FromSeconds(volumeStep));
        }
    }
}
