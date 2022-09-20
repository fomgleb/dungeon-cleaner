using System;
using System.Collections.Generic;
using System.Linq;
using Lean.Pool;
using MyExtensions;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace UnnamedGame.Dungeon.Scripts
{
    public class RandomTorchesInDungeonGenerator : MonoBehaviour
    { 
        [Header("Settings")]
        [Range(0, 1)] [SerializeField] private float occurrenceFrequency;
        [Header("Tilemaps")]
        [SerializeField] private Tilemap dungeonFloorTilemap;
        [SerializeField] private Tilemap torchTilemap;
        [SerializeField] private Tilemap sideTorchTilemap;
        [SerializeField] private Tilemap floorTilemap;
        [SerializeField] private Tilemap wallTilemap;
        [Header("Tiles")]
        [SerializeField] private TileBase torchTileBase;
        [SerializeField] private TileBase sideTileBase;
        [SerializeField] private GameObject torchPrefab;
        [SerializeField] private GameObject sideTorchPrefab;

        private List<GameObject> _spawnedTorches = new();

        private void OnEnable()
        {
            DungeonGeneratorBase.DungeonGeneratedEvent += OnDungeonGenerated;
        }

        private void OnDisable()
        {
            DungeonGeneratorBase.DungeonGeneratedEvent -= OnDungeonGenerated;
        }

        private void OnDungeonGenerated() => GenerateTorches();

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
                            spawnPoint = new Vector3(x, y + 1),
                            torchDirection = TorchDirection.Down
                        });
                    if (!floorTilemap.HasTile(new Vector3Int(x - 1, y)))
                        availableTorchData.Add(new InitTorchData()
                        {
                            spawnPoint = new Vector3(x, y),
                            torchDirection = TorchDirection.Right
                        });
                    if (!floorTilemap.HasTile(new Vector3Int(x + 1, y)))
                        availableTorchData.Add(new InitTorchData()
                        {
                            spawnPoint = new Vector3(x, y),
                            torchDirection =  TorchDirection.Left
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

        private void GenerateTorches()
        {
            foreach (var spawnedTorch in _spawnedTorches)
                LeanPool.Despawn(spawnedTorch);
            foreach (var torchData in GetRandomInitTorchData())
            {
                var spawningPrefab = torchData.torchDirection == TorchDirection.Down ? torchPrefab : sideTorchPrefab;
                var spawnedTorch = LeanPool.Spawn(spawningPrefab, torchData.spawnPoint + floorTilemap.tileAnchor,
                    Quaternion.identity, transform);
                if (torchData.torchDirection == TorchDirection.Left)
                    spawnedTorch.transform.localScale = new Vector3(-1, 1, 1);
                _spawnedTorches.Add((spawnedTorch));
            }
        }

        private struct InitTorchData
        {
            public Vector3 spawnPoint;
            public TorchDirection torchDirection;
        }

        private enum TorchDirection
        {
            Down,
            Right,
            Left
        }
    }
}
