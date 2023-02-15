using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Dungeon.Scripts;
using Game.Mouse.Scripts;
using UnityEngine;

namespace Game.Camera.Scripts
{
    public class CameraTarget : MonoBehaviour
    {
        [Range(2, 100)] [SerializeField] private float cameraTargetDivider;
        [SerializeField] private GameObjectSpawner playerSpawner;

        private Transform playerTransform;

        private CancellationTokenSource lookingAtMouseCancellationToken;

        private void Awake()
        {
            lookingAtMouseCancellationToken = new CancellationTokenSource();
        }

        private void OnEnable() => playerSpawner.SpawnedEvent += OnPlayerSpawned;
        private void OnDisable() => playerSpawner.SpawnedEvent -= OnPlayerSpawned;

        private void OnDestroy()
        {
            lookingAtMouseCancellationToken.Cancel();
        }

        private void OnPlayerSpawned()
        {
            playerTransform = playerSpawner.SpawnedObject.transform;
            LookAtMouseAsync(lookingAtMouseCancellationToken.Token);
        }

        public void StopLookingAtMouse() => lookingAtMouseCancellationToken.Cancel();

        private async void LookAtMouseAsync(CancellationToken token)
        {
            while (true)
            {
                if (token.IsCancellationRequested) return;

                var cameraTargetPosition = ((Vector3)MouseLocation.WorldPosition + (cameraTargetDivider - 1) *
                    playerTransform.position) / cameraTargetDivider;
                transform.position = cameraTargetPosition;

                await UniTask.Yield();
            }
        }
    }
}
