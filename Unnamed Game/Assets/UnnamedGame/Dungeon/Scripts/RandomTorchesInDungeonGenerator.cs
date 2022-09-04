using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace UnnamedGame.Dungeon.Scripts
{
    public class RandomTorchesInDungeonGenerator : MonoBehaviour
    {
        [SerializeField] private Tilemap dungeonFloorTilemap;
        [SerializeField] private Tilemap torchTilemap;
        [SerializeField] private Tilemap sideTorchTilemap;

        private void OnEnable()
        {
            DungeonGeneratorBase.DungeonGeneratedEvent += OnDungeonGenerated;
        }

        private void OnDisable()
        {
            DungeonGeneratorBase.DungeonGeneratedEvent -= OnDungeonGenerated;
        }

        private void OnDungeonGenerated() => GenerateTorches();

        private void GenerateTorches()
        {
            
        }
    }
}
