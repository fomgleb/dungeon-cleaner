using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace UnnamedGame.Dungeon.Scripts
{
    [RequireComponent(typeof(DungeonGeneratorBase))]
    public class RandomTorchesInDungeonGenerator : MonoBehaviour
    {
        [SerializeField] private Tilemap dungeonFloorTilemap;
        [SerializeField] private Tilemap torchTilemap;
        [SerializeField] private Tilemap sideTorchTilemap;

        private DungeonGeneratorBase _dungeonGenerator;

        private void Awake()
        {
            _dungeonGenerator = GetComponent<DungeonGeneratorBase>();
        }

        private void OnEnable()
        {
            _dungeonGenerator.DungeonGeneratedEvent += OnDungeonGenerated;
        }

        private void OnDisable()
        {
            _dungeonGenerator.DungeonGeneratedEvent -= OnDungeonGenerated;
        }

        private void OnDungeonGenerated() => GenerateTorches();

        public void GenerateTorches()
        {
            
        }
    }
}
