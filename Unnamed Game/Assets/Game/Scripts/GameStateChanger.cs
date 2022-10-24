using UnityEngine;
using UnnamedGame.Scene_Transition;

namespace UnnamedGame.Scripts
{
    public class GameStateChanger : MonoBehaviour
    {
        public void _MainMenu()
        {
            SceneTransition.SwitchScene("MainMenuScene");
        }
        
        public void _RestartGame()
        {
            SceneTransition.SwitchScene("DungeonScene");
        }

        public void _StartGame()
        {
            SceneTransition.SwitchScene("DungeonScene");
        }

        public void _ExitGame()
        {
            Application.Quit();
        }
    }
}
