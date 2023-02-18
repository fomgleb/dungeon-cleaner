using Game.Scripts.Scene_Transition;
using UnityEngine;

namespace Game.Scripts.Dungeon.Events
{
    public class ExitingEvent : MonoBehaviour
    {
        [SerializeField] private DungeonMusicPlayer dungeonMusicPlayer;

        private void OnEnable()
        {
            SceneTransition.SceneIsSwitchingEvent += OnSceneIsSwitching;
        }

        private void OnDisable()
        {
            SceneTransition.SceneIsSwitchingEvent -= OnSceneIsSwitching;
        }

        private void OnSceneIsSwitching()
        {
            dungeonMusicPlayer.StopSmoothly();
        }
    }
}
