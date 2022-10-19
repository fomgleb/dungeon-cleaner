using UnityEngine;
using UnnamedGame.Pause;
using Zenject;

namespace UnnamedGame.Mouse.Scripts
{
    public class RotatorToMouse : MonoBehaviour
    {
        [Inject] private MouseFollower _mouseFollower;
        [Inject] private Pauser pauser;

        private void Update()
        {
            if (pauser.IsPaused)
                return;
            
            Rotate();
        }

        private void Rotate()
        {
            var aimDirection = (_mouseFollower.transform.position - transform.position).normalized;
            var angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle);
            transform.localScale = angle is > 90 or < -90 ? new Vector3(1, -1, 1) : new Vector3(1, 1, 1);
        }
    }
}
