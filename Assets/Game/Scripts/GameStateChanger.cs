using Game.Scene_Transition;
using UnityEngine;

namespace Game.Scripts
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
