using Game.Scripts.Dungeon.Cave;
using Game.Scripts.Dungeon.Enemies_Spawner;
using Game.Scripts.Dungeon.Menus;
using Game.Scripts.Dungeon.Torches;
using Game.Scripts.Game_Object;
using Game.Scripts.Pause;
using Game.Scripts.Tile_Drawers;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

namespace Game.Scripts.Dungeon
{
    public class DungeonInit : MonoBehaviour
    {
        [SerializeField] private AudioMixerSnapshot inAimTipAudioMixerSnapshot;

        [FormerlySerializedAs("caveGenerationData")]
        [FormerlySerializedAs("dungeonGenerationData")]
        [Header("Settings")]
        [SerializeField] private DataOfCaveGenerationAlgorithm dataOfCaveGenerationAlgorithm;
        [Range(0, 1)] [SerializeField] private float torchesFrequency;

        [Space]
        [SerializeField] private DungeonMusicPlayer dungeonMusicPlayer;
        [SerializeField] private CaveTilesDrawer caveTilesDrawer;
        [SerializeField] private TorchTilesDrawer torchTilesDrawer;
        [SerializeField] private RandomEnemiesSpawner enemiesSpawner;
        [SerializeField] private GameObjectSpawner playerSpawner;
        [SerializeField] private SlimesCounter slimesCounter;

        [SerializeField] private Window playerHealthWindow;
        [SerializeField] private Window slimesCounterWindow;
        [SerializeField] private Window aimWindow;
        [SerializeField] private Window menuWindow;
        [SerializeField] private Window youDiedWindow;
        [SerializeField] private WinMenu winMenu;

        public CaveTilesDrawer CaveTilesDrawer => caveTilesDrawer;
        public TorchTilesDrawer TorchTilesDrawer => torchTilesDrawer;

        private void Start()
        {
            Pauser.ClearRegisteredHandlers();

            inAimTipAudioMixerSnapshot.TransitionTo(0);

            dungeonMusicPlayer.PlayRandomMusic();

            var dungeonGeneration = new DungeonGeneration(dataOfCaveGenerationAlgorithm, torchesFrequency);

            caveTilesDrawer.EraseAndDraw(dungeonGeneration.PositionsOfFloorTiles, dungeonGeneration.PositionsOfWallTiles);
            torchTilesDrawer.EraseAndDraw(dungeonGeneration.DataOfTorchTiles);
            enemiesSpawner._SpawnEnemies();
            playerSpawner.Spawn();
            slimesCounter.ShowEnemiesCount(enemiesSpawner.SpawnedEnemies.Count);

            playerHealthWindow.ShowAsync();
            slimesCounterWindow.ShowAsync();
            aimWindow.ShowAsync();
            menuWindow.SetVisibilityAbruptly(false);
            youDiedWindow.SetVisibilityAbruptly(false);
            winMenu.Hide();

            Pauser.SetPaused(true);
        }
    }
}