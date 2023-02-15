using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Window : MonoBehaviour
{
    [SerializeField] private float appearanceTime;
    [SerializeField] private float delay;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public async void ShowAsync()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(delay));

        gameObject.SetActive(true);

        if (appearanceTime != 0)
        {
            var appearanceSpeed = 1 / appearanceTime;
            for (var elapsedTime = 0f; elapsedTime < appearanceTime; elapsedTime += Time.deltaTime)
            {
                canvasGroup.alpha += appearanceSpeed * Time.deltaTime;
                await UniTask.Yield();
            }
        }

        canvasGroup.alpha = 1;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0;
        gameObject.SetActive(false);
    }
}
