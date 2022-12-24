using System.Collections.Specialized;
using Game.Audio.Scripts;
using Game.Dungeon.Scripts;
using Game.Entities.LivingEntities.Scripts;
using Game.Entities.Scripts;
using Game.Scene_Transition;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class DungeonMusicPlayer : MonoBehaviour
{
    [SerializeField] private LoopedMusic[] loopedMusic;
    [SerializeField] private EntitySpawner playerSpawner;

    private Animator animator;
    private AudioSource audioSource;
    
    private static readonly int DisappearTriggerName = Animator.StringToHash("Disappear");

    private Dieable playerDieable;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        SceneTransition.SceneIsSwitchingEvent += Disappear;
        playerSpawner.SpawnedEvent += OnPlayerSpawned;
        RandomEnemiesSpawner.SpawnedEnemies.CollectionChanged += OnSpawnedEnemiesCollectionChanged;
    }

    private void OnDisable()
    {
        SceneTransition.SceneIsSwitchingEvent -= Disappear;
        playerSpawner.SpawnedEvent -= OnPlayerSpawned;
        RandomEnemiesSpawner.SpawnedEnemies.CollectionChanged -= OnSpawnedEnemiesCollectionChanged;

        if (playerDieable != null)
            playerDieable.DiedEvent -= OnPlayerDied;
    }
    
    private void Start()
    {
        var randomMusicIndex = Random.Range(0, loopedMusic.Length);
        audioSource.clip = loopedMusic[randomMusicIndex].Clip;
        audioSource.time = loopedMusic[randomMusicIndex].StartTime;
        audioSource.Play();
    }

    private void OnPlayerSpawned()
    {
        playerDieable = playerSpawner.SpawnedObject.GetComponent<Dieable>();
        playerDieable.DiedEvent += OnPlayerDied;
    }

    private void OnPlayerDied(object sender, Dieable.DiedEventArgs eventArgs) => StopAbruptly();
    
    private void OnSpawnedEnemiesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.OldItems != null && RandomEnemiesSpawner.SpawnedEnemies.Count == 0)
            Disappear();
    }

    private void StopAbruptly()
    {
        audioSource.Stop();
    }

    private void Disappear()
    {
        if (audioSource.isPlaying)
            animator.SetTrigger(DisappearTriggerName);
    }
}
