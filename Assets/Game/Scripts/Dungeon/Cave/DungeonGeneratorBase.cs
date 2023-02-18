using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Scripts.Dungeon.Cave
{
    public abstract class DungeonGeneratorBase : MonoBehaviour
    {
        public static event Action DungeonGeneratedEvent;
        public static event Action DungeonDestroyedEvent;

        [Header("Dungeon Generator Base")]
        [SerializeField] protected bool clearBeforeGenerate;
        [SerializeField] protected Vector2Int startPosition = Vector2Int.zero;
        [SerializeField] protected Tilemap floorTilemap;
        [SerializeField] protected Tilemap wallTilemap;
        [SerializeField] protected Tilemap wallShadowTilemap;
        [SerializeField] protected TileBase floorRuleTile;
        [SerializeField] protected TileBase wallRuleTile;
        [SerializeField] protected TileBase wallShadowRuleTile;

        public void _GenerateDungeon()
        {
            if (clearBeforeGenerate)
            {
                ClearDungeon();
                DungeonDestroyedEvent?.Invoke();
            }
            RunProceduralGeneration();
            DungeonGeneratedEvent?.Invoke();
        }
        
        private void ClearDungeon()
        {
            floorTilemap.ClearAllTiles();
            wallTilemap.ClearAllTiles();
            wallShadowTilemap.ClearAllTiles();
        }

        protected abstract void RunProceduralGeneration();
    }
}
