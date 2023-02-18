using Game.Scripts.Audio;
using UnityEngine;

namespace Game.Scripts.Dungeon
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Animator))]
    public class DungeonMusicPlayer : MonoBehaviour
    {
        [SerializeField] private LoopedMusic[] loopedMusic;

        private Animator animator;
        private AudioSource audioSource;
        private static readonly int DisappearTriggerName = Animator.StringToHash("Disappear");

        private GameObject playerGameObject;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayRandomMusic()
        {
            var randomMusicIndex = Random.Range(0, loopedMusic.Length);
            audioSource.clip = loopedMusic[randomMusicIndex].Clip;
            audioSource.time = loopedMusic[randomMusicIndex].StartTime;
            audioSource.Play();
        }
    
        public void StopAbruptly() => audioSource.Stop();

        public void StopSmoothly()
        {
            if (audioSource.isPlaying)
                animator.SetTrigger(DisappearTriggerName);
        }
    }
}
