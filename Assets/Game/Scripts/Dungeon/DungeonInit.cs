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

        [SerializeField] private Window playerHealthWindow;
        [SerializeField] private Window slimesCounterWindow;
        [SerializeField] private Window aimWindow;
        [SerializeField] private Window menuWindow;
        [SerializeField] private Window youDiedWindow;
        [SerializeField] private WinMenu winMenu;

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

            playerHealthWindow.ShowAsync();
            playerHealthWindow.ShowAsync();
            aimWindow.ShowAsync();
            menuWindow.SetVisibilityAbruptly(false);
            youDiedWindow.SetVisibilityAbruptly(false);
            winMenu.Hide();

            Pauser.SetPaused(true);
        }
    }
}