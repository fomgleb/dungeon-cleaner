using System.Collections.Generic;
using System.Linq;
using Lean.Pool;
using MyExtensions;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Game.Dungeon.Scripts
{
    public class RandomTorchesInDungeonGenerator : MonoBehaviour
    { 
        [Header("Settings")]
        [Range(0, 1)] [SerializeField] private float occurrenceFrequency;
        [Header("Tilemaps")]
        [SerializeField] private Tilemap floorTilemap;
        [SerializeField] private Tilemap wallTilemap;
        [Header("Tiles")]
        [SerializeField] private GameObject torchPrefab;
        [SerializeField] private GameObject sideTorchPrefab;

        [Inject] private DiContainer diContainer;

        private readonly List<GameObject> spawnedTorches = new();

        private List<Vector2Int> GetRandomTorchPositions()
        {
            var cellPositionsOfAllTiles = wallTilemap.GetCellPositionsOfAllTiles();
            var availableTorchPositions = new List<Vector2Int>();
            foreach (var cellPosition in cellPositionsOfAllTiles)
            {
                var positionUnderCurrentCell = new Vector2Int(cellPosition.x, cellPosition.y - 1);
                if (cellPosition.x >= floorTilemap.cellBounds.xMax || cellPosition.x <= floorTilemap.cellBounds.xMin ||
                    cellPosition.y <= floorTilemap.cellBounds.yMin ||
                    cellPosition.y > floorTilemap.cellBounds.yMax) continue;
                if (cellPositionsOfAllTiles.Any(position => position == positionUnderCurrentCell)) continue;
                availableTorchPositions.Add(cellPosition);
            }
            availableTorchPositions.Shuffle();

            var randomTorchPositions = availableTorchPositions.GetRange(0, (int)(availableTorchPositions.Count * occurrenceFrequency));
            
            return randomTorchPositions;
        }

        private List<InitTorchData> GetRandomInitTorchData()
        {
            var availableTorchData = new List<InitTorchData>();
            
            for (var y = floorTilemap.cellBounds.yMin; y <= floorTilemap.cellBounds.yMax; y++)
                for (var x = floorTilemap.cellBounds.xMin; x <= floorTilemap.cellBounds.xMax; x++)
                {
                    if (!floorTilemap.HasTile(new Vector3Int(x, y))) continue;
                    if (!floorTilemap.HasTile(new Vector3Int(x, y + 1)))
                        availableTorchData.Add(new InitTorchData()
                        {
                            SpawnPoint = new Vector3(x, y + 1),
                            TorchDirection = TorchDirection.Down
                        });
                    if (!floorTilemap.HasTile(new Vector3Int(x - 1, y)))
                        availableTorchData.Add(new InitTorchData()
                        {
                            SpawnPoint = new Vector3(x, y),
                            TorchDirection = TorchDirection.Right
                        });
                    if (!floorTilemap.HasTile(new Vector3Int(x + 1, y)))
                        availableTorchData.Add(new InitTorchData()
                        {
                            SpawnPoint = new Vector3(x, y),
                            TorchDirection =  TorchDirection.Left
                        });
                }
            availableTorchData.Shuffle();
            return availableTorchData.GetRange(0, (int)(availableTorchData.Count * occurrenceFrequency));
        }

        // private List<Vector2Int> GetRandomSideTorchPositions()
        // {
        //     var cellPositionsOfAllTiles = floorTilemap.GetCellPositionsOfAllTiles();
        //     var availableSideTorchPositions = new List<Vector2Int>();
        //     foreach (var cellPosition in cellPositionsOfAllTiles)
        //     {
        //         
        //     }
        // }

        public void _GenerateTorches()
        {
            foreach (var spawnedTorch in spawnedTorches)
                if (Application.isEditor)
                    DestroyImmediate(spawnedTorch);
                else
                    LeanPool.Despawn(spawnedTorch);
            foreach (var torchData in GetRandomInitTorchData())
            {
                var spawningPrefab = torchData.TorchDirection == TorchDirection.Down ? torchPrefab : sideTorchPrefab;
                // var spawnedTorch = Application.isEditor
                //     ? Instantiate(spawningPrefab, torchData.spawnPoint + floorTilemap.tileAnchor, Quaternion.identity, transform)
                //     : diContainer.InstantiatePrefab(spawningPrefab, torchData.spawnPoint + floorTilemap.tileAnchor, Quaternion.identity, transform);
                var spawnedTorch = diContainer.InstantiatePrefab(spawningPrefab, torchData.SpawnPoint + floorTilemap.tileAnchor, Quaternion.identity, transform);
                if (torchData.TorchDirection == TorchDirection.Left)
                    spawnedTorch.transform.localScale = new Vector3(-1, 1, 1);
                spawnedTorches.Add((spawnedTorch));
            }
        }

        private struct InitTorchData
        {
            public Vector3 SpawnPoint;
            public TorchDirection TorchDirection;
        }

        private enum TorchDirection
        {
            Down,
            Right,
            Left
        }
    }
}
