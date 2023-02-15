using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Game.Entities.LivingEntities.Scripts;
using MyExtensions;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Dungeon.Scripts
{
    public class RandomEnemiesSpawner : MonoBehaviour
    {
        [SerializeField] private Tilemap spawningZoneTilemap;
        [SerializeField] private Vector2Int[] forbiddenSpawnPositions;
        [SerializeField] private Transform enemiesParentOnScene;
        [Tooltip("Inclusive")] [SerializeField] private uint minNumberOfEnemies;
        [Tooltip("Inclusive")] [SerializeField] private uint maxNumberOfEnemies;
        [SerializeField] private SpawningEnemyData[] spawningEnemiesData;

        public event Action AllEnemiesDiedEvent;

        public ObservableCollection<GameObject> SpawnedEnemies { get; } = new();

        [Inject] private DiContainer diContainer;

        public void _DespawnEnemies()
        {
            foreach (var spawnedEnemy in SpawnedEnemies)
                Destroy(spawnedEnemy);
            SpawnedEnemies.Clear();
        }

        public void _SpawnEnemies()
        {
            var randomNumberOfSpawningEnemies = Random.Range((int)minNumberOfEnemies, (int)(maxNumberOfEnemies + 1));
            var cellPositionsOfAllTiles = spawningZoneTilemap.GetCellPositionsOfAllTiles();

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
                    var spawnedEnemy = diContainer.InstantiatePrefab(spawningEnemyData.EnemyPrefab, enemiesParentOnScene);
                    spawnedEnemy.transform.position = randomSpawnWorldPositionsQueue.Dequeue();
                    SpawnedEnemies.Add(spawnedEnemy);
                    spawnedEnemy.GetComponent<Damageable>().DiedEvent += OnEnemyDied;
                }
            }
        }

        private void OnEnemyDied(object sender, Damageable.DiedEventArgs diedEventArgs)
        {
            SpawnedEnemies.Remove((GameObject)sender);
            if (SpawnedEnemies.Count == 0)
                AllEnemiesDiedEvent?.Invoke();
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
