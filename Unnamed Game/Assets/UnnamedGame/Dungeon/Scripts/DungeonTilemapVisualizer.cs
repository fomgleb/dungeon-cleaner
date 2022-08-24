using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace UnnamedGame.Dungeon.Scripts
{
    public class DungeonTilemapVisualizer : MonoBehaviour
    {
        [SerializeField] private Tilemap floorTilemap;
        [SerializeField] private Tilemap wallTilemap;
        [SerializeField] private Tilemap wallShadowTilemap;
        [SerializeField] private RuleTile floorRuleTile;
        [SerializeField] private RuleTile wallRuleTile;
        [SerializeField] private RuleTile wallShadowRuleTile;

        public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
        {
            PaintTiles(floorPositions, floorTilemap, floorRuleTile);
        }

        public void PaintWallTiles(IEnumerable<Vector2Int> wallPositions)
        {
            PaintTiles(wallPositions, wallTilemap, wallRuleTile);
        }

        public void PaintWallShadowTiles(IEnumerable<Vector2Int> wallShadowPositions)
        {
            PaintTiles(wallShadowPositions, wallShadowTilemap, wallShadowRuleTile);
        }
        
        public void Clear()
        {
            floorTilemap.ClearAllTiles();
            wallTilemap.ClearAllTiles();
            wallShadowTilemap.ClearAllTiles();
        }

        private static void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
        {
            foreach (var position in positions)
                PaintSingleTile(tilemap, tile, position);
        }

        private static void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
        {
            var tilePosition = tilemap.WorldToCell((Vector3Int)position);
            tilemap.SetTile(tilePosition, tile);
        }
    }
}
