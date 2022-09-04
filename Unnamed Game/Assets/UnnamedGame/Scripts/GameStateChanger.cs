using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnnamedGame.Scripts
{
    public class GameStateChanger : MonoBehaviour
    {
        public void RestartGame()
        {
            SceneManager.LoadScene("DungeonScene");
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
