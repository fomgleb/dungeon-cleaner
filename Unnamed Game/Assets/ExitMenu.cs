using UnityEngine;

public class ExitMenu : MonoBehaviour
{
    [SerializeField] private GameObject exitButton;
    
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            exitButton.SetActive(!exitButton.activeSelf);
    }
}
