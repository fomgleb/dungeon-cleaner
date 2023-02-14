using Game.Pause;
using UnityEngine;

namespace Game.Mouse.Scripts
{
    public class MouseFollower : MonoBehaviour
    {
        [SerializeField] private new UnityEngine.Camera camera;

        private void Start()
        {
            transform.position = Vector3.zero;
        }

        private void Update()
        {
            if (Pauser.IsPaused)
                return;
            var newMousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = newMousePosition;
        }
    }
}
