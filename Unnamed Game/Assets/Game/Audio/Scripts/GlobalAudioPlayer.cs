using Lean.Pool;
using UnityEngine;

namespace Game.Global.Audio.Scripts
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
    }
}
