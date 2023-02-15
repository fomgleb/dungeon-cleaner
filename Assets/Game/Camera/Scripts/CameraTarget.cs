using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Dungeon.Scripts;
using Game.Entities.LivingEntities.Scripts;
using Game.Mouse.Scripts;
using UnityEngine;
using Zenject;

namespace Game.Camera.Scripts
{
    public class CameraTarget : MonoBehaviour
    {
        [Range(2, 100)] [SerializeField] private float cameraTargetDivider;
        [SerializeField] private GameObjectSpawner playerSpawner;

        [Inject] private MouseFollower mouseFollower;

        private Transform playerTransform;
        private Transform mouseTransform;

        private CancellationTokenSource lookingAtMouseCancellationToken;

        private void Awake()
        {
            mouseTransform = mouseFollower.transform;
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
            playerSpawner.SpawnedObject.GetComponent<Damageable>().DiedEvent += OnPlayerDied;
            playerTransform = playerSpawner.SpawnedObject.transform;
            LookAtMouseAsync(lookingAtMouseCancellationToken.Token);
        }

        private void OnPlayerDied(object sender, Damageable.DiedEventArgs diedEventArgs)
        {
            lookingAtMouseCancellationToken.Cancel();
        }

        private async void LookAtMouseAsync(CancellationToken token)
        {
            while (true)
            {
                if (token.IsCancellationRequested) return;

                var cameraTargetPosition = (mouseTransform.position + (cameraTargetDivider - 1) *
                    playerTransform.position) / cameraTargetDivider;
                transform.position = cameraTargetPosition;

                await UniTask.Yield();
            }
        }
    }
}
