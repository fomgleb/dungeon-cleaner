using UnityEditor;
using UnityEngine;

namespace Game.Scripts.Dungeon.Cave.Editor
{
    [CustomEditor(typeof(CaveTilesDrawer), true), CanEditMultipleObjects]
    public class CaveTilesDrawerEditor : UnityEditor.Editor
    {
        private CaveTilesDrawer caveTilesDrawer;
        
        private void OnEnable()
        {
            caveTilesDrawer = (CaveTilesDrawer)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Generate Cave"))
            {
                var dungeonGeneration = new DungeonGeneration(caveTilesDrawer.DataOfCaveGenerationAlgorithm);
                var positionsOfFloorTiles = dungeonGeneration.GeneratePositionsOfFloorTiles();
                var positionsOfWallTiles = dungeonGeneration.GeneratePositionsOfWallTiles(positionsOfFloorTiles);
                caveTilesDrawer.Erase();
                caveTilesDrawer.Draw(positionsOfFloorTiles, positionsOfWallTiles);
            }
        }
    }
}