using UnityEngine;
using UnnamedGame.Pause;
using Zenject;

public class GameUIAndHUDController : MonoBehaviour
{
    [SerializeField] private GameObject health;
    [SerializeField] private GameObject slimesCounter;
    [SerializeField] private GameObject aimMessage;

    [Inject] private Pauser pauser;

    private void Start()
    {
        TurnAimMessage(true);
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            TurnAimMessage(false);
        }
    }

    private void TurnAimMessage(bool state)
    {
        pauser.SetPaused(state);
        health.SetActive(!state);
        slimesCounter.SetActive(!state);
        aimMessage.SetActive(state);
    }
}
