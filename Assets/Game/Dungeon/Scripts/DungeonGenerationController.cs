using UnityEngine;

namespace Game.Dungeon.Scripts
{
    public class DungeonGenerationController : MonoBehaviour
    {
        [SerializeField] private DungeonGeneratorBase dungeonGenerator;
        [SerializeField] private RandomTorchesInDungeonGenerator torchesGenerator;
        [SerializeField] private RandomEnemiesSpawner enemiesSpawner;

        private void OnEnable()
        {
            DungeonGeneratorBase.DungeonDestroyedEvent += OnDungeonDestroyed;
        }
        
        private void OnDisable()
        {
            DungeonGeneratorBase.DungeonDestroyedEvent -= OnDungeonDestroyed;
        }

        private void Start()
        {
            dungeonGenerator._GenerateDungeon();
            torchesGenerator._GenerateTorches();
            enemiesSpawner._SpawnEnemies();
        }

        private void OnDungeonDestroyed() => enemiesSpawner._DespawnEnemies();
    }
}
