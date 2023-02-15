using Game.Pause;
using UnityEngine;

namespace Game.Mouse.Scripts
{
    public class MouseLocation : MonoBehaviour
    {
        [SerializeField] private new UnityEngine.Camera camera;
        
        public static Vector2 WorldPosition { get; private set; } 

        private void Update()
        {
            if (Pauser.IsPaused)
                return;
            WorldPosition = camera.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
