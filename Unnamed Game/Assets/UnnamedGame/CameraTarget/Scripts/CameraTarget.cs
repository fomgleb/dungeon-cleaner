using System;
using UnityEngine;

namespace UnnamedGame.CameraTarget.Scripts
{
    public class CameraTarget : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform mouseTransform;
        [Range(2, 100)] [SerializeField] private float cameraTargetDivider;

        private float _aspectRatio;
        
        private void Start()
        {
            //_aspectRatio = (float)Screen.currentResolution.width / Screen.currentResolution.height;
        }

        private void Update()
        {
            var directionFromPlayerToMouse = (Vector2)(mouseTransform.position - playerTransform.position);
            var distanceBetweenPlayerAndMouse = directionFromPlayerToMouse.magnitude;
            
            var correctedMousePosition = new Vector3(mouseTransform.position.x, mouseTransform.position.y + distanceBetweenPlayerAndMouse * _aspectRatio, mouseTransform.position.z);

            
            
            var cameraTargetPosition = (correctedMousePosition + (cameraTargetDivider - 1) * playerTransform.position) /
                                       cameraTargetDivider;
            
            transform.position = cameraTargetPosition;
        }
    }
}
