using Game.Scripts.Tile_Drawers;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Scripts.Dungeon.Cave.Editor
{
    [CustomEditor(typeof(DungeonInit), true), CanEditMultipleObjects]
    public class DungeonInitEditor : UnityEditor.Editor
    {
        private DungeonInit dungeonInit;

        private SerializedProperty caveTilesDrawerSerializedProperty;
        private SerializedProperty torchTilesDrawerSerializedProperty;
        private SerializedProperty testDataOfCaveGenerationAlgorithmSerializedProperty;
        private SerializedProperty floorTilemapSerializedProperty;
        private SerializedProperty testTorchesFrequencySerializedProperty;

        private CaveTilesDrawer caveTilesDrawer;
        private TorchTilesDrawer torchTilesDrawer;
        private DataOfCaveGenerationAlgorithm testDataOfCaveGenerationAlgorithm;
        private Tilemap floorTilemap;
        private float testTorchesFrequency;

        private void OnEnable()
        {
            dungeonInit = (DungeonInit)target;

            caveTilesDrawerSerializedProperty = serializedObject.FindProperty(nameof(caveTilesDrawer));
            torchTilesDrawerSerializedProperty = serializedObject.FindProperty(nameof(torchTilesDrawer));
            testDataOfCaveGenerationAlgorithmSerializedProperty = serializedObject.FindProperty(nameof(testDataOfCaveGenerationAlgorithm));
            floorTilemapSerializedProperty = serializedObject.FindProperty(nameof(floorTilemap));
            testTorchesFrequencySerializedProperty = serializedObject.FindProperty(nameof(testTorchesFrequency));
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            caveTilesDrawer = (CaveTilesDrawer)caveTilesDrawerSerializedProperty.objectReferenceValue;
            torchTilesDrawer = (TorchTilesDrawer)torchTilesDrawerSerializedProperty.objectReferenceValue;
            testDataOfCaveGenerationAlgorithm = (DataOfCaveGenerationAlgorithm)testDataOfCaveGenerationAlgorithmSerializedProperty.objectReferenceValue;
            floorTilemap = (Tilemap)floorTilemapSerializedProperty.objectReferenceValue;
            testTorchesFrequency = testTorchesFrequencySerializedProperty.floatValue;

            if (GUILayout.Button("Generate Dungeon"))
            {
                var dungeonGeneration = new DungeonGeneration(testDataOfCaveGenerationAlgorithm, testTorchesFrequency);
                caveTilesDrawer.EraseAndDraw(dungeonGeneration.PositionsOfFloorTiles, dungeonGeneration.PositionsOfWallTiles);
                torchTilesDrawer.EraseAndDraw(dungeonGeneration.DataOfTorchTiles);
            }
        }
    }
}