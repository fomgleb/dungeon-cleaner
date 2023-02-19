using System.Collections.Generic;
using Game.Scripts.Dungeon.Cave;
using Game.Scripts.Tiles;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Scripts
{
    public class CaveTilesDrawer : MonoBehaviour
    {
        [SerializeField] protected Tilemap floorTilemap;
        [SerializeField] protected Tilemap wallTilemap;
        [SerializeField] protected Tilemap wallShadowTilemap;
        [SerializeField] protected TileBase floorRuleTile;
        [SerializeField] protected TileBase wallRuleTile;
        [SerializeField] protected TileBase wallShadowRuleTile;
        [Header("Test")]
        [SerializeField] private DungeonGenerationData dungeonGenerationData;

        public DungeonGenerationData DungeonGenerationData => dungeonGenerationData;

        public void EraseAndDraw( HashSet<Vector2Int> positionsOfFloorTiles, HashSet<Vector2Int> positionsOfWallTiles)
        {
            Erase();
            Draw(positionsOfFloorTiles, positionsOfWallTiles);
        }
        
        public void Draw(HashSet<Vector2Int> positionsOfFloorTiles, HashSet<Vector2Int> positionsOfWallTiles)
        {
            TilesPainter.PaintTiles(positionsOfFloorTiles, floorTilemap, floorRuleTile);
            TilesPainter.PaintTiles(positionsOfWallTiles, wallTilemap, wallRuleTile);
            TilesPainter.PaintTiles(positionsOfFloorTiles, wallShadowTilemap, wallShadowRuleTile);
        }

        public void Erase()
        {
            floorTilemap.ClearAllTiles();
            wallTilemap.ClearAllTiles();
            wallShadowTilemap.ClearAllTiles();
        }
    }
}