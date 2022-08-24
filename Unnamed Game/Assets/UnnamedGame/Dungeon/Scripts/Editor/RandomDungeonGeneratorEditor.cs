using UnityEditor;
using UnityEngine;

namespace UnnamedGame.Dungeon.Scripts.Editor
{
    [CustomEditor(typeof(DungeonGeneratorBase), true), CanEditMultipleObjects]
    public class RandomDungeonGeneratorEditor : UnityEditor.Editor
    {
        private DungeonGeneratorBase _dungeonGeneratorBase;
        
        private void Awake()
        {
            _dungeonGeneratorBase = (DungeonGeneratorBase)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Create Dungeon"))
            {
                _dungeonGeneratorBase._GenerateDungeon();
            }
        }
    }
}