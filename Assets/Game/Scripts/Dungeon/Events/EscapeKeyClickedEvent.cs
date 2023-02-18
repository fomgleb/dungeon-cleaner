using Game.Scripts.Dungeon.Menus;
using UnityEngine;

namespace Game.Scripts.Dungeon.Events
{
    public class EscapeKeyClickedEvent : MonoBehaviour
    {
        [SerializeField] private Window menuWindow;
    
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                OnEscapeKeyClicked();
        }

        private void OnEscapeKeyClicked()
        {
            menuWindow.SetVisibilityAbruptly(!menuWindow.IsVisible);
        }
    }
}
