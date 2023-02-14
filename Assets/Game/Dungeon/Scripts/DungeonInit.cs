using Game.Pause;
using UnityEngine;

namespace Game.Dungeon.Scripts
{
    public class DungeonInit : MonoBehaviour
    {
        [SerializeField] private DungeonGeneratorBase caveGenerator;
        [SerializeField] private RandomTorchesInDungeonGenerator torchesGenerator;
        [SerializeField] private RandomEnemiesSpawner enemiesSpawner;
        [SerializeField] private GameObjectSpawner playerSpawner;

        private void Start()
        {
            Pauser.ClearRegisteredHandlers();
            caveGenerator._GenerateDungeon();
            torchesGenerator._GenerateTorches();
            enemiesSpawner._SpawnEnemies();
            playerSpawner.Spawn();
        }
    }
}
