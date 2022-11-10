using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private static FPSCounter instance;

    [SerializeField] private TMP_Text text;
    [SerializeField] private float updateDelay; 
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            UpdateFPSCounterAsync();
        }
    }

    private async void UpdateFPSCounterAsync()
    {
        while (true)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(updateDelay));
            var current = (int)(1f / Time.unscaledDeltaTime);
            text.text = current.ToString();
        }
    }
}
