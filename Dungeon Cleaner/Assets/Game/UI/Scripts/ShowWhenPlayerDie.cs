using Cysharp.Threading.Tasks;
using Game.Entities.LivingEntities.Scripts;
using UnityEngine;
using UnnamedGame.LivingEntities.Player.Scripts;
using Zenject;

namespace Game.UI.Scripts
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ShowWhenPlayerDie : MonoBehaviour
    {
        [SerializeField] private float appearanceTime;
        [SerializeField] private float delay;
        [SerializeField] private GameObject[] objectsToEnable;

        [Inject] private PlayerInput playerInput;
    
        private CanvasGroup canvasGroup;
        private Dieable playerDamageable;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            playerDamageable = playerInput.GetComponent<Dieable>();
        }

        private void OnEnable()
        {
            playerDamageable.DiedEvent += OnDied;
        }

        private void OnDisable()
        {
            playerDamageable.DiedEvent -= OnDied;
        }

        private void OnDied(object sender, Dieable.DiedEventArgs eventArgs) => Show();

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
