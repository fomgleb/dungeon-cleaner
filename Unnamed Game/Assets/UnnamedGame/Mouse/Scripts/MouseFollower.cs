using UnityEngine;

namespace UnnamedGame.Mouse.Scripts
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
            var newMousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = newMousePosition;
        }
    }
}
