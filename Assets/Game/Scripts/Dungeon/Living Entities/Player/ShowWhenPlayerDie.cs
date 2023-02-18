using Cysharp.Threading.Tasks;
using Game.Scripts.Game_Object;
using UnityEngine;

namespace Game.Scripts.Dungeon.Living_Entities.Player
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ShowWhenPlayerDie : MonoBehaviour
    {
        [SerializeField] private float appearanceTime;
        [SerializeField] private float delay;
        [SerializeField] private GameObject[] objectsToEnable;
        [SerializeField] private GameObjectSpawner playerSpawner;

        private CanvasGroup canvasGroup;
        private Damageable playerDamageable;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnEnable() => playerSpawner.SpawnedEvent += OnPlayerSpawned; 

        private void OnDisable() => playerSpawner.SpawnedEvent -= OnPlayerSpawned;

        private void OnPlayerSpawned()
        {
            playerDamageable = playerSpawner.SpawnedObject.GetComponent<Damageable>();
            playerDamageable.DiedEvent += OnPlayerDied;
        }

        private void OnPlayerDied(object sender, Damageable.DiedEventArgs diedEventArgs) => Show();

        private async void Show()
        {
            await UniTask.Delay((int)(delay * 1000));

            foreach (var objectToEnable in objectsToEnable)
                objectToEnable.SetActive(true);

            var appearanceSpeed = 1 / appearanceTime;
        
            for (var elapsedTime = 0f; elapsedTime < appearanceTime; elapsedTime += Time.deltaTime)
            {
                canvasGroup.alpha += appearanceSpeed * Time.deltaTime;
                await UniTask.Yield();
            }
        }
    }
}
