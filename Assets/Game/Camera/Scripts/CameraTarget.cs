using Game.Entities.LivingEntities.Player.Scripts;
using Game.Mouse.Scripts;
using UnityEngine;
using Zenject;

namespace Game.Camera.Scripts
{
    public class CameraTarget : MonoBehaviour
    {
        [Range(2, 100)] [SerializeField] private float cameraTargetDivider;

        [Inject] private PlayerInput playerInput;
        [Inject] private MouseFollower mouseFollower;

        private Transform playerTransform;
        private Transform mouseTransform;
        
        private void Awake()
        {
            playerTransform = playerInput.transform;
            mouseTransform = mouseFollower.transform;
        }

        private void Update()
        {
            if (playerInput == null) return;
            var cameraTargetPosition = (mouseTransform.position + (cameraTargetDivider - 1) * playerTransform.position) /
                                       cameraTargetDivider;
            
            transform.position = cameraTargetPosition;
        }
    }
}
