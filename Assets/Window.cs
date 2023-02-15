using Cysharp.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Window : MonoBehaviour
{
    [SerializeField] private float appearanceTime;

    private Transform[] childrenTransforms;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        childrenTransforms = GetComponentsInChildren<Transform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public async void ShowAsync()
    {
        foreach (var childrenObject in childrenTransforms)
            childrenObject.gameObject.SetActive(true);

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
        foreach (var childrenTransform in childrenTransforms)
            childrenTransform.gameObject.SetActive(false);
    }
}
