using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MyExtensions
{
    public static class TilemapExtensions
    {
        public static IEnumerable<Vector2Int> GetCellPositionsOfAllTiles(this Tilemap tilemap)
        {
            var allTilesPositions = new List<Vector2Int>();
            foreach (var tileCellPositionInBounds in tilemap.cellBounds.allPositionsWithin)
                if (tilemap.HasTile(tileCellPositionInBounds))
                    allTilesPositions.Add((Vector2Int)tileCellPositionInBounds);
            return allTilesPositions;
        }
    }
}