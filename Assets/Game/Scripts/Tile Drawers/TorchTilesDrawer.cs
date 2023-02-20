using System;
using System.Collections.Generic;
using Game.Scripts.Dungeon;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Scripts.Tile_Drawers
{
    public class TorchTilesDrawer : MonoBehaviour
    {
        [Header("Tilemaps")] [SerializeField] private Tilemap topTorchTilemap;
        [SerializeField] private Tilemap rightTorchTilemap;
        [SerializeField] private Tilemap leftTorchTilemap;

        [Header("Tile Rules")] [SerializeField] private RuleTile topTorchRuleTile;
        [SerializeField] private RuleTile rightTorchRuleTile;
        [SerializeField] private RuleTile leftTorchRuleTile;

        public void EraseAndDraw(List<DungeonGeneration.Torch> dataOfSpawningTorches)
        {
            Erase();
            Draw(dataOfSpawningTorches);
        }

        public void Draw(List<DungeonGeneration.Torch> dataOfSpawningTorches)
        {
            foreach (var dataOfSpawningTorch in dataOfSpawningTorches)
                switch (dataOfSpawningTorch.Direction)
                {
                    case DungeonGeneration.Torch.DirectionEnum.Top:
                        TilesDrawer.Draw(dataOfSpawningTorch.Position, topTorchTilemap, topTorchRuleTile);
                        break;
                    case DungeonGeneration.Torch.DirectionEnum.Right:
                        TilesDrawer.Draw(dataOfSpawningTorch.Position, rightTorchTilemap, rightTorchRuleTile);
                        break;
                    case DungeonGeneration.Torch.DirectionEnum.Left:
                        TilesDrawer.Draw(dataOfSpawningTorch.Position, leftTorchTilemap, leftTorchRuleTile);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
        }

        public void Erase()
        {
            topTorchTilemap.ClearAllTiles();
            rightTorchTilemap.ClearAllTiles();
            leftTorchTilemap.ClearAllTiles();
        }
    }
}