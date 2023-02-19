using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Scripts.Tiles
{
    public abstract class TilesPainter
    {
        public static void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
        {
            foreach (var position in positions)
            {
                var tilePosition = tilemap.WorldToCell((Vector3Int)position);
                tilemap.SetTile(tilePosition, tile);
            }
        }

        public static void Draw(Vector2Int position, Tilemap tilemap, TileBase tile)
        {
            var tilePosition = tilemap.WorldToCell((Vector3Int)position);
            tilemap.SetTile(tilePosition, tile);
        }
    }
}
