using Game.Pause;
using Game.UI.Scripts;
using UnityEngine;

namespace Game.Dungeon.Scripts
{
    public class DungeonInit : MonoBehaviour
    {
        [SerializeField] private DungeonMusicPlayer dungeonMusicPlayer;
        [SerializeField] private DungeonGeneratorBase caveGenerator;
        [SerializeField] private RandomTorchesInDungeonGenerator torchesGenerator;
        [SerializeField] private RandomEnemiesSpawner enemiesSpawner;
        [SerializeField] private GameObjectSpawner playerSpawner;
        [SerializeField] private SlimesCounter slimesCounter;

        private void Start()
        {
            Pauser.ClearRegisteredHandlers();

            dungeonMusicPlayer.PlayRandomMusic();
            caveGenerator._GenerateDungeon();
            torchesGenerator._GenerateTorches();
            enemiesSpawner._SpawnEnemies();
            playerSpawner.Spawn();
            slimesCounter.ShowEnemiesCount(enemiesSpawner.SpawnedEnemies.Count);
        }
    }
}
