using System.Collections.Generic;
using System.Linq;
using Game.Dungeon.Data.Scripts;
using Game.Tiles.Scripts;
using MyExtensions;
using UnityEngine;

namespace Game.Dungeon.Scripts
{
    public class SimpleRandomWalkDungeonGenerator : DungeonGeneratorBase
    {
        [Space]
        [SerializeField] private DungeonGenerationData dungeonGenerationData;

        protected override void RunProceduralGeneration()
        {
            var floorPositions =
                ProceduralGenerationAlgorithms.SimpleRandomWalk(startPosition, dungeonGenerationData.NumberOfSteps,
                    dungeonGenerationData.StepsForOneDirection, dungeonGenerationData.ChanceToTurn);
            var wallPositions = GetWallPositions(floorPositions);

            if (!clearBeforeGenerate)
            {
                wallPositions.ExceptWith(floorTilemap.GetCellPositionsOfAllTiles());
                TilesPainter.PaintTiles(floorPositions.Where(pos => wallTilemap.HasTile((Vector3Int)pos)), wallTilemap, null);
            }
            
            TilesPainter.PaintTiles(floorPositions, floorTilemap, floorRuleTile);
            TilesPainter.PaintTiles(wallPositions, wallTilemap, wallRuleTile);
            TilesPainter.PaintTiles(floorPositions, wallShadowTilemap, wallShadowRuleTile);
        }

        private static HashSet<Vector2Int> GetWallPositions(HashSet<Vector2Int> floorPositions)
        {
            var leftTopWallPosition = new Vector2Int(floorPositions.Min(position => position.x),
                floorPositions.Max(position => position.y)) + new Vector2Int(-10, 10);
            var rightBottomWallPosition = new Vector2Int(floorPositions.Max(position => position.x),
                floorPositions.Min(position => position.y)) + new Vector2Int(10, -10);
            var wallPositions = new HashSet<Vector2Int>();
            for (var x = leftTopWallPosition.x; x <= rightBottomWallPosition.x; x++)
            for (var y = rightBottomWallPosition.y; y <= leftTopWallPosition.y; y++)
                wallPositions.Add(new Vector2Int(x, y));

            foreach (var floorPosition in floorPositions)
                wallPositions.Remove(floorPosition);

            return wallPositions;
        }
    }
}
