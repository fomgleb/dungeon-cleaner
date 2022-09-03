using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRestarter : MonoBehaviour
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
