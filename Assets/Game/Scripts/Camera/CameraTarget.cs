using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Scripts.Mouse;
using UnityEngine;

namespace Game.Scripts.Camera
{
    public class CameraTarget : MonoBehaviour
    {
        [Range(2, 100)] [SerializeField] private float cameraTargetDivider;

        private CancellationTokenSource lookingAtMouseCancellationToken;

        private void Awake()
        {
            lookingAtMouseCancellationToken = new CancellationTokenSource();
        }

        private void OnDestroy()
        {
            lookingAtMouseCancellationToken.Cancel();
        }

        public void StopLookingAtMouse() => lookingAtMouseCancellationToken.Cancel();

        public async void LookAtMouseAsync(Transform playerTransform)
        {
            while (true)
            {
                if (lookingAtMouseCancellationToken.IsCancellationRequested) return;

                var cameraTargetPosition = ((Vector3)MouseLocation.WorldPosition + (cameraTargetDivider - 1) *
                    playerTransform.position) / cameraTargetDivider;
                transform.position = cameraTargetPosition;

                await UniTask.Yield();
            }
        }
    }
}
