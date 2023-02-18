using Game.Scripts.Dungeon.Cave;
using UnityEditor;
using UnityEngine;

namespace Game.Dungeon.Scripts.Editor
{
    [CustomEditor(typeof(DungeonGeneratorBase), true), CanEditMultipleObjects]
    public class RandomDungeonGeneratorEditor : UnityEditor.Editor
    {
        private DungeonGeneratorBase dungeonGeneratorBase;
        
        private void Awake()
        {
            dungeonGeneratorBase = (DungeonGeneratorBase)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Create Dungeon"))
            {
                dungeonGeneratorBase._GenerateDungeon();
            }
        }
    }
}
