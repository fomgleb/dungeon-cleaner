using Game.Scripts.Dungeon.Menus;
using Game.Scripts.Pause;
using UnityEngine;
using UnityEngine.Audio;

namespace Game.Scripts.Dungeon.Events
{
    public class ClickedForTheFirstTimeEvent : MonoBehaviour
    {
        [SerializeField] private AudioMixerSnapshot normalAudioMixerSnapshot;
        [SerializeField] private Window aimWindow;
    
        private bool clickedForTheFirstTime;

        private void Update()
        {
            if (!clickedForTheFirstTime && Input.anyKey)
            {
                clickedForTheFirstTime = true;
            
                normalAudioMixerSnapshot.TransitionTo(0.5f);
                aimWindow.SetVisibilityAbruptly(false);
                Pauser.SetPaused(false);
            }
        }
    }
}
