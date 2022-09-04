using Lean.Pool;
using UnityEngine;

namespace UnnamedGame.Audio.Scripts
{
    public class GlobalAudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource audioUnitPrefab;
        
        public void PlayAudio(AudioClip audioClip, Vector2 position)
        {
            var spawnedAudioUnit = LeanPool.Spawn(audioUnitPrefab, position, Quaternion.identity, transform);
            spawnedAudioUnit.PlayOneShot(audioClip);
            LeanPool.Despawn(spawnedAudioUnit, audioClip.length);
        }
    }
}
