using Game.Audio.Scripts;
using Game.Dungeon.Scripts;
using Game.Scene_Transition;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class DungeonMusicPlayer : MonoBehaviour
{
    [SerializeField] private LoopedMusic[] loopedMusic;
    [SerializeField] private RandomEnemiesSpawner enemiesSpawner;

    private Animator animator;
    private AudioSource audioSource;
    private static readonly int DisappearTriggerName = Animator.StringToHash("Disappear");

    private GameObject playerGameObject;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        SceneTransition.SceneIsSwitchingEvent += Disappear;
        enemiesSpawner.AllEnemiesDiedEvent += OnAllEnemiesDied;
    }

    private void OnDisable()
    {
        SceneTransition.SceneIsSwitchingEvent -= Disappear;
        enemiesSpawner.AllEnemiesDiedEvent -= OnAllEnemiesDied;
    }

    public void PlayRandomMusic()
    {
        var randomMusicIndex = Random.Range(0, loopedMusic.Length);
        audioSource.clip = loopedMusic[randomMusicIndex].Clip;
        audioSource.time = loopedMusic[randomMusicIndex].StartTime;
        audioSource.Play();
    }
    
    private void OnAllEnemiesDied() => Disappear();

    public void StopAbruptly() => audioSource.Stop();

    private void Disappear()
    {
        if (audioSource.isPlaying)
            animator.SetTrigger(DisappearTriggerName);
    }
}
