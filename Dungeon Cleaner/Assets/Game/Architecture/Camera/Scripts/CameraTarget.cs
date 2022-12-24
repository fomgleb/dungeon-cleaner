using Game.Mouse.Scripts;
using UnityEngine;
using UnnamedGame.LivingEntities.Player.Scripts;
using Zenject;

namespace Game.Architecture.Camera.Scripts
{
    public class CameraTarget : MonoBehaviour
    {
        [Range(2, 100)] [SerializeField] private float cameraTargetDivider;

        [Inject] private PlayerInput _playerInput;
        [Inject] private MouseFollower _mouseFollower;

        private Transform _playerTransform;
        private Transform _mouseTransform;
        
        private void Awake()
        {
            _playerTransform = _playerInput.transform;
            _mouseTransform = _mouseFollower.transform;
        }

        private void Update()
        {
            if (_playerInput == null) return;
            var cameraTargetPosition = (_mouseTransform.position + (cameraTargetDivider - 1) * _playerTransform.position) /
                                       cameraTargetDivider;
            
            transform.position = cameraTargetPosition;
        }
    }
}
