using UnityEngine;

namespace UnnamedGame.CameraTarget.Scripts
{
    public class CameraTarget : MonoBehaviour
    {
        [SerializeField] private Transform playerPosition;
        [SerializeField] private Transform mousePosition;
        [Range(2, 100)] [SerializeField] private float cameraTargetDivider;

        private void Update()
        {
            var cameraTargetPosition = (mousePosition.position + (cameraTargetDivider - 1) * playerPosition.position) /
                                       cameraTargetDivider;
            
            transform.position = cameraTargetPosition;
        }
    }
}
