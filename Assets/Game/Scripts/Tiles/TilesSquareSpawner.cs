using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Tiles.Scripts
{
    public class TilesSquareSpawner : MonoBehaviour
    {
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private TileBase tile;
        [SerializeField] private Vector2Int leftDown;
        [SerializeField] private Vector2Int rightUp;

        public void _Spawn()
        {
            for (var y = leftDown.y; y < rightUp.y; y++)
            {
                for (var x = leftDown.x; x < rightUp.x; x++)
                {
                    var tilePosition = tilemap.WorldToCell(new Vector3Int(x, y));
                    tilemap.SetTile(tilePosition, tile);
                }
            }
        }
    }
}
