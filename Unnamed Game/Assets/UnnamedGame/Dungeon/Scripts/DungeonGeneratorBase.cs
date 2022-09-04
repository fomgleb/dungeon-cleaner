using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace UnnamedGame.Dungeon.Scripts
{
    public abstract class DungeonGeneratorBase : MonoBehaviour
    {
        public static event Action DungeonGeneratedEvent;
        public static event Action DungeonDestroyedEvent;
        
        [Header("Dungeon Generator Base")]
        [SerializeField] protected Vector2Int startPosition = Vector2Int.zero;
        [SerializeField] protected Tilemap floorTilemap;
        [SerializeField] protected Tilemap wallTilemap;
        [SerializeField] protected Tilemap wallShadowTilemap;
        [SerializeField] protected TileBase floorRuleTile;
        [SerializeField] protected TileBase wallRuleTile;
        [SerializeField] protected TileBase wallShadowRuleTile;

        public void _GenerateDungeon()
        {
            ClearDungeon();
            DungeonDestroyedEvent?.Invoke();
            RunProceduralGeneration();
            DungeonGeneratedEvent?.Invoke();
        }
        
        private void ClearDungeon()
        {
            floorTilemap.ClearAllTiles();
            wallTilemap.ClearAllTiles();
            wallShadowTilemap.ClearAllTiles();
        }

        protected static void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
        {
            foreach (var position in positions)
            {
                var tilePosition = tilemap.WorldToCell((Vector3Int)position);
                tilemap.SetTile(tilePosition, tile);
            }
        }

        protected abstract void RunProceduralGeneration();
    }
}
