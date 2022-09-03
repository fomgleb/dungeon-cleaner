using Cysharp.Threading.Tasks;
using UnityEngine;
using UnnamedGame.LivingEntities.Player.Scripts;
using UnnamedGame.LivingEntities.Scripts;
using Zenject;

[RequireComponent(typeof(CanvasGroup))]
public class ShowWhenPlayerDie : MonoBehaviour
{
    [SerializeField] private float appearanceTime;
    [SerializeField] private float delay;
    [SerializeField] private GameObject[] objectsToEnable;

    [Inject] private PlayerInput _playerInput;
    
    private CanvasGroup _canvasGroup;
    private Damageable _playerDamageable;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _playerDamageable = _playerInput.GetComponent<Damageable>();
    }

    private void OnEnable()
    {
        _playerDamageable.DiedEvent += OnDied;
    }

    private void OnDisable()
    {
        _playerDamageable.DiedEvent -= OnDied;
    }

    private void OnDied(Damageable damageable) => Show();

    private async void Show()
    {
        await UniTask.Delay((int)(delay * 1000));

        foreach (var objectToEnable in objectsToEnable)
            objectToEnable.SetActive(true);

        var appearanceSpeed = 1 / appearanceTime;
        
        for (var elapsedTime = 0f; elapsedTime < appearanceTime; elapsedTime += Time.deltaTime)
        {
            _canvasGroup.alpha += appearanceSpeed * Time.deltaTime;
            await UniTask.Yield();
        }
    }
}
