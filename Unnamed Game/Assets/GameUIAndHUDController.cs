using Game.Pause;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

public class GameUIAndHUDController : MonoBehaviour
{
    [SerializeField] private GameObject health;
    [SerializeField] private GameObject slimesCounter;
    [SerializeField] private GameObject aimMessage;
    [SerializeField] private AudioMixerSnapshot normal;
    [SerializeField] private AudioMixerSnapshot inAimTip;
    
    
    [Inject] private Pauser pauser;

    

    private void Start()
    {
        TurnAimMessage(true);
        inAimTip.TransitionTo(0);
    }

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
        pauser.SetPaused(state);
        health.SetActive(!state);
        slimesCounter.SetActive(!state);
        aimMessage.SetActive(state);
    }
}
