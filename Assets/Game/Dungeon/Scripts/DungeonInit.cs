using Game.Pause;
using Game.UI.Scripts;
using UnityEngine;
using UnityEngine.Audio;

namespace Game.Dungeon.Scripts
{
    public class DungeonInit : MonoBehaviour
    {
        [SerializeField] private AudioMixerSnapshot inAimTipAudioMixerSnapshot;

        [SerializeField] private DungeonMusicPlayer dungeonMusicPlayer;
        [SerializeField] private DungeonGeneratorBase caveGenerator;
        [SerializeField] private RandomTorchesInDungeonGenerator torchesGenerator;
        [SerializeField] private RandomEnemiesSpawner enemiesSpawner;
        [SerializeField] private GameObjectSpawner playerSpawner;
        [SerializeField] private SlimesCounter slimesCounter;

        [SerializeField] private Window aimWindow;
        [SerializeField] private Window menuWindow;
        [SerializeField] private Window youDiedWindow;

        private void Start()
        {
            Pauser.ClearRegisteredHandlers();

            inAimTipAudioMixerSnapshot.TransitionTo(0);

            dungeonMusicPlayer.PlayRandomMusic();
            caveGenerator._GenerateDungeon();
            torchesGenerator._GenerateTorches();
            enemiesSpawner._SpawnEnemies();
            playerSpawner.Spawn();
            slimesCounter.ShowEnemiesCount(enemiesSpawner.SpawnedEnemies.Count);

            aimWindow.ShowAsync();
            menuWindow.Hide();
            youDiedWindow.Hide();

            Pauser.SetPaused(true);
        }
    }
}