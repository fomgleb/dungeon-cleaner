using Cysharp.Threading.Tasks;
using UnityEngine;
using UnnamedGame.LivingEntities.Scripts;

[RequireComponent(typeof(CanvasGroup))]
public class ShowWhenDie : MonoBehaviour
{
    [SerializeField] private Damageable damageable;
    [SerializeField] private float appearanceTime;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        damageable.DiedEvent += OnDied;
    }

    private void OnDisable()
    {
        damageable.DiedEvent -= OnDied;
    }

    private void OnDied() => Show();

    private async void Show()
    {
        var appearanceSpeed = 1 / appearanceTime;
        
        for (var elapsedTime = 0f; elapsedTime < appearanceTime; elapsedTime += Time.deltaTime)
        {
            _canvasGroup.alpha += appearanceSpeed * Time.deltaTime;
            await UniTask.Yield();
        }
    }
}
