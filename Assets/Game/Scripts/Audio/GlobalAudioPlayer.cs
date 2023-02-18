using Lean.Pool;
using UnityEngine;

namespace Game.Scripts.Audio
{
    public class GlobalAudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource audioUnitPrefab;
        
        public void PlayAudio(AudioClip audioClip, Vector2 position, float minRandomPitch, float maxRandomPitch)
        {
            var spawnedAudioUnit = LeanPool.Spawn(audioUnitPrefab, position, Quaternion.identity, transform);
            spawnedAudioUnit.pitch = Random.Range(minRandomPitch, maxRandomPitch);
            spawnedAudioUnit.PlayOneShot(audioClip);
            LeanPool.Despawn(spawnedAudioUnit, audioClip.length + 1);
        }

        public void PlayAudio(Sound sound, Vector2 position)
        {
            var spawnedAudioUnit = LeanPool.Spawn(audioUnitPrefab, position, Quaternion.identity, transform);
            spawnedAudioUnit.pitch = Random.Range(sound.MinRandomPitch, sound.MaxRandomPitch);
            var randomAudioClip = sound.RandomAudioClips[Random.Range(0, sound.RandomAudioClips.Length)];
            spawnedAudioUnit.PlayOneShot(randomAudioClip);
            LeanPool.Despawn(spawnedAudioUnit, randomAudioClip.length + 1);
        }
    }
}
