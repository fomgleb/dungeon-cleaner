using Game.Pause;
using UnityEngine;
using Zenject;

namespace Game.Mouse.Scripts
{
    public class MouseFollower : MonoBehaviour
    {
        [SerializeField] private new Camera camera;

        [Inject] private Pauser pauser;

        private void Start()
        {
            transform.position = Vector3.zero;
        }

        private void Update()
        {
            if (pauser.IsPaused)
                return;
            var newMousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = newMousePosition;
        }
    }
}
