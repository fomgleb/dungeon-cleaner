using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MyExtensions;
using NTC.Global.Pool;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnnamedGame.LivingEntities.Scripts;
using Zenject;
using Random = UnityEngine.Random;

namespace UnnamedGame.Dungeon.Scripts
{
    public class RandomEnemiesSpawner : MonoBehaviour
    {
        [SerializeField] private Tilemap spawningZoneTilemap;
        [SerializeField] private Vector2Int[] forbiddenSpawnPositions;
        [SerializeField] private Transform enemiesParentOnScene;
        [Tooltip("Inclusive")] [SerializeField] private uint minNumberOfEnemies;
        [Tooltip("Inclusive")] [SerializeField] private uint maxNumberOfEnemies;
        [SerializeField] private SpawningEnemyData[] spawningEnemiesData;

        public static ObservableCollection<GameObject> SpawnedEnemies { get; } = new();

        [Inject] private DiContainer _diContainer;

        private void OnEnable()
        {
            DungeonGeneratorBase.DungeonGeneratedEvent += OnDungeonGenerated;
            DungeonGeneratorBase.DungeonDestroyedEvent += OnDungeonDestroyed;
        }

        private void OnDisable()
        {
            DungeonGeneratorBase.DungeonGeneratedEvent -= OnDungeonGenerated;
            DungeonGeneratorBase.DungeonDestroyedEvent -= OnDungeonDestroyed;
        }

        private void OnDungeonGenerated() => SpawnEnemies();
        private void OnDungeonDestroyed() => DespawnEnemies();

        private void DespawnEnemies()
        {
            foreach (var spawnedEnemy in SpawnedEnemies)
                NightPool.Despawn(spawnedEnemy);
            SpawnedEnemies.Clear();
        }

        private void SpawnEnemies()
        {
            var randomNumberOfSpawningEnemies = Random.Range((int)minNumberOfEnemies, (int)(maxNumberOfEnemies + 1));
            var cellPositionsOfAllTiles = GetCellPositionsOfAllTiles(spawningZoneTilemap);

            var availableSpawnCellPositions = cellPositionsOfAllTiles.Except(forbiddenSpawnPositions).ToList();
            availableSpawnCellPositions.Shuffle();

            var randomSpawnCellPositions = availableSpawnCellPositions.GetRange(0, randomNumberOfSpawningEnemies); 

            var randomSpawnWorldPositionsQueue = new Queue<Vector2>();
            foreach (var randomSpawnCellPosition in randomSpawnCellPositions)
            {
                var randomSpawnWorldPosition = spawningZoneTilemap.CellToWorld((Vector3Int)randomSpawnCellPosition) +
                                               spawningZoneTilemap.tileAnchor;
                randomSpawnWorldPositionsQueue.Enqueue(randomSpawnWorldPosition);
            }

            foreach (var spawningEnemyData in spawningEnemiesData)
            {
                var numberOfThisTypeEnemies = (int)Mathf.Round(spawningEnemyData.SpawnChance * randomNumberOfSpawningEnemies);
                for (var j = 0; j < numberOfThisTypeEnemies; j++)
                {
                    var spawnedEnemy = _diContainer.InstantiatePrefab(spawningEnemyData.EnemyPrefab, enemiesParentOnScene);
                    spawnedEnemy.transform.position = randomSpawnWorldPositionsQueue.Dequeue();
                    SpawnedEnemies.Add(spawnedEnemy);
                    spawnedEnemy.GetComponent<Damageable>().DiedEvent += OnEnemyDied;
                }
            }
        }

        private void OnEnemyDied(Damageable enemyDamageable)
        {
            SpawnedEnemies.Remove(enemyDamageable.gameObject);
        }

        private static IEnumerable<Vector2Int> GetCellPositionsOfAllTiles(Tilemap tilemap)
        {
            var allTilesPositions = new List<Vector2Int>();
            foreach (var tileCellPositionInBounds in tilemap.cellBounds.allPositionsWithin)
                if (tilemap.HasTile(tileCellPositionInBounds))
                    allTilesPositions.Add((Vector2Int)tileCellPositionInBounds);
            return allTilesPositions;
        }

        [Serializable]
        private struct SpawningEnemyData
        {
            [SerializeField] private GameObject enemyPrefab;
            [SerializeField, Range(0, 1)] private float spawnChance;

            public GameObject EnemyPrefab => enemyPrefab;
            public float SpawnChance => spawnChance;
        }
    }
}
