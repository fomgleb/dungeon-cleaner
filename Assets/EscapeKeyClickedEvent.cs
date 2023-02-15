using UnityEngine;

public class EscapeKeyClickedEvent : MonoBehaviour
{
    [SerializeField] private Window menuWindow;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OnEscapeKeyClicked();
    }

    private void OnEscapeKeyClicked()
    {
        menuWindow.ShowAsync();
    }
}
