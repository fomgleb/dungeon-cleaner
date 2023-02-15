using Game.Pause;
using UnityEngine;
using UnityEngine.Audio;

public class GameUIAndHUDController : MonoBehaviour
{
    [SerializeField] private GameObject aimMessage;
    [SerializeField] private AudioMixerSnapshot normal;
    
    private void Update()
    {
        if (Input.anyKey)
        {
            TurnAimMessage(false);
            normal.TransitionTo(0.5f);
        }
    }

    private void TurnAimMessage(bool state)
    {
        Pauser.SetPaused(state);
        aimMessage.SetActive(state);
    }
}
