using UnityEditor;
using UnityEngine;

namespace UnnamedGame.UI.MainMenu.Scripts.Editor
{
    [CustomEditor(typeof(MainMenuGenerationController), true), CanEditMultipleObjects]
    public class MainMenuGenerationControllerEditor : UnityEditor.Editor
    {
        private MainMenuGenerationController mainMenuGenerationController;

        private void Awake()
        {
            mainMenuGenerationController = (MainMenuGenerationController)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Create Dungeon Menu"))
            {
                mainMenuGenerationController._GenerateMainMenu();
            }
        }
    }
}
