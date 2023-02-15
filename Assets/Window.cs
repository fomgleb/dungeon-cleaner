using UnityEngine;

public class Window : MonoBehaviour
{
    private Transform[] childrenTransforms;

    private void Awake()
    {
        childrenTransforms = GetComponentsInChildren<Transform>();
    }

    public void Show()
    {
        foreach (var childrenObject in childrenTransforms)
            childrenObject.gameObject.SetActive(true);
    }

    public void Hide()
    {
        foreach (var childrenTransform in childrenTransforms)
            childrenTransform.gameObject.SetActive(false);
    }
}
