using UnityEngine;

namespace UnnamedGame.Scripts
{
    public class RotatorForTarget : MonoBehaviour
    {
        [SerializeField] private Transform targetTransform;

        private void Update()
        {
            Rotate();
        }

        private void Rotate()
        {
            var aimDirection = (targetTransform.position - transform.position).normalized;
            var angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle);
            transform.localScale = angle is > 90 or < -90 ? new Vector3(1, -1, 1) : new Vector3(1, 1, 1);
        }
    }
}
