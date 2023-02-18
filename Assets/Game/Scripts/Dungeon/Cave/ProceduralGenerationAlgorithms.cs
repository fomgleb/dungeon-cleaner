using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Scripts.Dungeon.Cave
{
    public static class ProceduralGenerationAlgorithms
    {
        public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength,
            int numberOfStepsForOneDirection, float chanceToTurn)
        {
            var path = new HashSet<Vector2Int> { startPosition };

            var previousPosition = startPosition;
            var numberOfStepsInOneDirection = 0;
            var currentDirection = Direction2D.GetRandomCardinalDirection();

            while (path.Count < walkLength)
            {
                var newPosition = previousPosition + currentDirection;
                path.Add(newPosition);
                previousPosition = newPosition;
                numberOfStepsInOneDirection++;
                if (Random.Range(0f, 1f) < chanceToTurn || numberOfStepsInOneDirection >= numberOfStepsForOneDirection)
                {
                    var availableDirections = Direction2D.CardinalDirectionsList
                        .Where(direction => direction != currentDirection).ToArray();
                    currentDirection = availableDirections[Random.Range(0, availableDirections.Count())];
                    numberOfStepsInOneDirection = 0;
                }
            }

            return path;
        }

        public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
        {
            var corridor = new List<Vector2Int>();
            var direction = Direction2D.GetRandomCardinalDirection();
            var currentPosition = startPosition;
            corridor.Add(currentPosition);

            for (var i = 0; i < corridorLength; i++)
            {
                currentPosition += direction;
                corridor.Add(currentPosition);
            }

            return corridor;
        }
    }

    public static class Direction2D
    {
        public static readonly List<Vector2Int> CardinalDirectionsList = new List<Vector2Int>
        {
            new Vector2Int(0, 1), // UP
            new Vector2Int(1, 0), // RIGHT
            new Vector2Int(0, -1), // DOWN
            new Vector2Int(-1, 0) // LEFT
        };

        public static readonly List<Vector2Int> CornersList = new List<Vector2Int>
        {
            new(1, 1), // TOP RIGHT
            new Vector2Int(1, -1), // BOTTOM RIGHT
            new Vector2Int(-1, -1), // BOTTOM LEFT
            new Vector2Int(-1, 1) // TOP LEFT
        };

        public static Vector2Int GetRandomCardinalDirection()
        {
            return CardinalDirectionsList[Random.Range(0, CardinalDirectionsList.Count)];
        } 
    }
}
