using UnityEngine;

namespace Game.Scripts.Dungeon.Menus
{
    public class ExitMenu : MonoBehaviour
    {
        [SerializeField] private GameObject exitButton;
    
        private void Update()
        {
            if (Input.GetButtonDown("Cancel"))
                exitButton.SetActive(!exitButton.activeSelf);
        }
    }
}
