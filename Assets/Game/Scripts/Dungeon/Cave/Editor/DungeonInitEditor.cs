using UnityEditor;
using UnityEngine;

namespace Game.Scripts.Dungeon.Cave.Editor
{
    [CustomEditor(typeof(DungeonInit), true), CanEditMultipleObjects]
    public class DungeonInitEditor : UnityEditor.Editor
    {
        private DungeonInit dungeonInit;

        private DataOfCaveGenerationAlgorithm dataOfCaveGenerationAlgorithm;
        private float torchesFrequency;

        private void OnEnable()
        {
            dungeonInit = (DungeonInit)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(10);
            EditorGUILayout.LabelField("Test", EditorStyles.boldLabel);
            dataOfCaveGenerationAlgorithm = (DataOfCaveGenerationAlgorithm)EditorGUILayout.ObjectField(
                dataOfCaveGenerationAlgorithm,
                typeof(DataOfCaveGenerationAlgorithm), false);
            torchesFrequency = EditorGUILayout.Slider("Torches frequency", torchesFrequency, 0, 1);

            if (GUILayout.Button("Generate Dungeon"))
            {
                var dungeonGeneration = new DungeonGeneration(dataOfCaveGenerationAlgorithm, torchesFrequency);
                dungeonInit.CaveTilesDrawer.EraseAndDraw(dungeonGeneration.PositionsOfFloorTiles,
                    dungeonGeneration.PositionsOfWallTiles);
                dungeonInit.TorchTilesDrawer.EraseAndDraw(dungeonGeneration.DataOfTorchTiles);
            }
        }
    }
}