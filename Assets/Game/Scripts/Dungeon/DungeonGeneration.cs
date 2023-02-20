using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Dungeon.Cave;
using MyExtensions;
using UnityEngine;

namespace Game.Scripts.Dungeon
{
    public class DungeonGeneration
    {
        public HashSet<Vector2Int> PositionsOfFloorTiles { get; }
        public HashSet<Vector2Int> PositionsOfWallTiles { get; }
        public List<Torch> DataOfTorchTiles { get; }

        private readonly Vector2Int startPosition;
        private readonly uint numberOfSteps;
        private readonly uint stepsForOneDirection;
        private readonly float chanceToTurn;
        private readonly float torchesFrequency;

        public DungeonGeneration(DataOfCaveGenerationAlgorithm caveData, float torchesFrequency)
        {
            startPosition = caveData.StartPosition;
            numberOfSteps = caveData.NumberOfSteps;
            stepsForOneDirection = caveData.StepsForOneDirection;
            chanceToTurn = caveData.ChanceToTurn;
            this.torchesFrequency = torchesFrequency;

            PositionsOfFloorTiles = GeneratePositionsOfFloorTiles();
            PositionsOfWallTiles = GeneratePositionsOfWallTiles(PositionsOfFloorTiles);
            DataOfTorchTiles = GeneratePositionsOfTorches(PositionsOfFloorTiles);
        }

        public Vector2Int FindFurthestPoint(IEnumerable<Vector2Int> positions, Vector2Int startPosition)
        {
            var startPoint = new Point(startPosition, 0);
            var furthestPoint = startPoint;

            var allPoints = positions.Select(p => new Point(p, -1)).ToHashSet();
            allPoints.Remove(new Point(startPoint.Position, -1));
            allPoints.Add(startPoint);

            var processingPoints = new Queue<Point>();
            processingPoints.Enqueue(startPoint);

            while (processingPoints.Count != 0)
            {
                var currentPoint = processingPoints.Dequeue();
                var positionsAround = new[]
                {
                    currentPoint.Position + Vector2Int.up,
                    currentPoint.Position + Vector2Int.right,
                    currentPoint.Position + Vector2Int.down,
                    currentPoint.Position + Vector2Int.left
                };

                foreach (var position in positionsAround)
                {
                    var removingPoint = new Point(position, -1);
                    if (allPoints.Contains(removingPoint))
                    {
                        allPoints.Remove(removingPoint);
                        var addingPoint = new Point(position, currentPoint.DistanceFromStart + 1);
                        allPoints.Add(addingPoint);
                        processingPoints.Enqueue(addingPoint);
                        if (furthestPoint.DistanceFromStart < addingPoint.DistanceFromStart)
                            furthestPoint = addingPoint;
                    }
                }
            }

            return furthestPoint.Position;
        }

        private struct Point
        {
            public readonly Vector2Int Position;
            public int DistanceFromStart { get; set; }

            public Point(Vector2Int position, int distanceFromStart)
            {
                Position = position;
                DistanceFromStart = distanceFromStart;
            }
        }

        private HashSet<Vector2Int> GeneratePositionsOfFloorTiles()
        {
            var positionsOfFloorTiles = new HashSet<Vector2Int> { startPosition };

            var previousPosition = startPosition;
            var numberOfStepsInOneDirection = 0;
            var currentDirection = Direction2D.GetRandomCardinalDirection();

            while (positionsOfFloorTiles.Count < numberOfSteps)
            {
                var newPosition = previousPosition + currentDirection;
                positionsOfFloorTiles.Add(newPosition);
                previousPosition = newPosition;
                numberOfStepsInOneDirection++;
                if (Random.Range(0f, 1f) < chanceToTurn || numberOfStepsInOneDirection >= stepsForOneDirection)
                {
                    var availableDirections = Direction2D.CardinalDirectionsList
                        .Where(direction => direction != currentDirection).ToArray();
                    currentDirection = availableDirections[Random.Range(0, availableDirections.Count())];
                    numberOfStepsInOneDirection = 0;
                }
            }

            return positionsOfFloorTiles;
        }

        private HashSet<Vector2Int> GeneratePositionsOfWallTiles(HashSet<Vector2Int> positionsOfFloorTiles)
        {
            var leftTopWallPosition = new Vector2Int(positionsOfFloorTiles.Min(position => position.x),
                positionsOfFloorTiles.Max(position => position.y)) + new Vector2Int(-10, 10);
            var rightBottomWallPosition = new Vector2Int(positionsOfFloorTiles.Max(position => position.x),
                positionsOfFloorTiles.Min(position => position.y)) + new Vector2Int(10, -10);
            var positionsOfWallTiles = new HashSet<Vector2Int>();
            for (var x = leftTopWallPosition.x; x <= rightBottomWallPosition.x; x++)
            for (var y = rightBottomWallPosition.y; y <= leftTopWallPosition.y; y++)
                positionsOfWallTiles.Add(new Vector2Int(x, y));

            foreach (var floorPosition in positionsOfFloorTiles)
                positionsOfWallTiles.Remove(floorPosition);

            return positionsOfWallTiles;
        }

        private List<Torch> GeneratePositionsOfTorches(HashSet<Vector2Int> positionsOfFloorTiles)
        {
            var availablePositionsOfTorches = new List<Torch>();

            foreach (var floorTilePosition in positionsOfFloorTiles)
            {
                if (!positionsOfFloorTiles.Contains(floorTilePosition + Vector2Int.up))
                    availablePositionsOfTorches.Add(new Torch(floorTilePosition + Vector2Int.up,
                        Torch.DirectionEnum.Top));

                if (!positionsOfFloorTiles.Contains(floorTilePosition + Vector2Int.left))
                    availablePositionsOfTorches.Add(new Torch(floorTilePosition, Torch.DirectionEnum.Left));

                if (!positionsOfFloorTiles.Contains(floorTilePosition + Vector2Int.right))
                    availablePositionsOfTorches.Add(new Torch(floorTilePosition, Torch.DirectionEnum.Right));
            }

            availablePositionsOfTorches.Shuffle();
            return availablePositionsOfTorches.GetRange(0, (int)(availablePositionsOfTorches.Count * torchesFrequency));
        }

        public struct Torch
        {
            public readonly Vector2Int Position;
            public readonly DirectionEnum Direction;

            public Torch(Vector2Int position, DirectionEnum direction)
            {
                Position = position;
                Direction = direction;
            }

            public enum DirectionEnum
            {
                Top,
                Right,
                Left
            }
        }

        private static class Direction2D
        {
            public static readonly List<Vector2Int> CardinalDirectionsList = new List<Vector2Int>
            {
                new Vector2Int(0, 1), // UP
                new Vector2Int(1, 0), // RIGHT
                new Vector2Int(0, -1), // DOWN
                new Vector2Int(-1, 0) // LEFT
            };

            public static Vector2Int GetRandomCardinalDirection()
            {
                return CardinalDirectionsList[Random.Range(0, CardinalDirectionsList.Count)];
            }
        }
    }
}