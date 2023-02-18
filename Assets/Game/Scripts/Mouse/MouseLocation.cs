using Game.Scripts.Pause;
using UnityEngine;

namespace Game.Scripts.Mouse
{
    public class MouseLocation : MonoBehaviour
    {
        [SerializeField] private new UnityEngine.Camera camera;
        
        public static Vector2 WorldPosition { get; private set; }

        private void Awake()
        {
            WorldPosition = Vector2.zero;
        }

        private void Update()
        {
            if (Pauser.IsPaused)
                return;
            WorldPosition = camera.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
