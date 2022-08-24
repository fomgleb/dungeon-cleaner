using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

namespace UnnamedGame.Dungeon.Scripts
{
    public static class WallsFinder
    {
        public static Stopwatch _stopwatch;

        static WallsFinder()
        {
            _stopwatch = new Stopwatch();
        }
        
        public static HashSet<Vector2Int> FindWallsAroundFloors(IEnumerable<Vector2Int> floorPositions)
        {
            var wallPositions = new HashSet<Vector2Int>();
            foreach (var floorPosition in floorPositions)
            {
                for (var y = -10; y < 11; y++)
                {
                    for (var x = -10; x < 11; x++)
                    {
                        var neighbourPosition = floorPosition + new Vector2Int(x, y);
                        _stopwatch.Start();
                        //if (!floorPositions.Contains(neighbourPosition))
                        wallPositions.Add(neighbourPosition);
                        
                        
                        
                        _stopwatch.Stop();
                    }
                }
                
                foreach (var direction in Direction2D.CardinalDirectionsList.Union(Direction2D.CornersList))
                {
                    
                }
            }

            return wallPositions;
        }
    }
}