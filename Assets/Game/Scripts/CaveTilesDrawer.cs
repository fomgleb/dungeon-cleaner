using System.Collections.Generic;
using Game.Scripts.Dungeon.Cave;
using Game.Scripts.Tiles;
using UnityEngine;
using UnityEngine.Serialization;
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
        [FormerlySerializedAs("caveGenerationData")]
        [FormerlySerializedAs("dungeonGenerationData")]
        [Header("Test")]
        [SerializeField] private DataOfCaveGenerationAlgorithm dataOfCaveGenerationAlgorithm;

        public DataOfCaveGenerationAlgorithm DataOfCaveGenerationAlgorithm => dataOfCaveGenerationAlgorithm;

        public void EraseAndDraw( HashSet<Vector2Int> positionsOfFloorTiles, HashSet<Vector2Int> positionsOfWallTiles)
        {
            Erase();
            Draw(positionsOfFloorTiles, positionsOfWallTiles);
        }
        
        public void Draw(HashSet<Vector2Int> positionsOfFloorTiles, HashSet<Vector2Int> positionsOfWallTiles)
        {
            TilesDrawer.Draw(positionsOfFloorTiles, floorTilemap, floorRuleTile);
            TilesDrawer.Draw(positionsOfWallTiles, wallTilemap, wallRuleTile);
            TilesDrawer.Draw(positionsOfFloorTiles, wallShadowTilemap, wallShadowRuleTile);
        }

        public void Erase()
        {
            floorTilemap.ClearAllTiles();
            wallTilemap.ClearAllTiles();
            wallShadowTilemap.ClearAllTiles();
        }
    }
}