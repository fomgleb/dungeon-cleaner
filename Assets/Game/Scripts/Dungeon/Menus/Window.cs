using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Window : MonoBehaviour
{
    [SerializeField] private float appearanceTime;
    [SerializeField] private float delay;
    
    public bool IsVisible { get; private set; }

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public async void ShowAsync()
    {
        IsVisible = true;
        
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

    public void SetVisibilityAbruptly(bool isVisible)
    {
        IsVisible = isVisible;
        gameObject.SetActive(isVisible);
        canvasGroup.alpha = isVisible ? 1 : 0;
    }
}
