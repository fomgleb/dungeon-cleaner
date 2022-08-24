using UnityEngine;

namespace UnnamedGame.Mouse.Scripts
{
    public class MouseFollower : MonoBehaviour
    {
        [SerializeField] private new Camera camera;
        
        private void Update()
        {
            transform.position = camera.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
