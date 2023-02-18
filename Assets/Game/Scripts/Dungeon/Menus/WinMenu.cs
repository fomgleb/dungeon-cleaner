using UnityEngine;

namespace Game.Scripts.Dungeon.Menus
{
    [RequireComponent(typeof(Window))]
    public class WinMenu : MonoBehaviour
    {
        [SerializeField] private Animator likerAnimator;

        private static readonly int StartLikingTriggerName = Animator.StringToHash("StartLiking");

        private Window window;

        private void Awake()
        {
            window = GetComponent<Window>();
        }

        public void Show()
        {
            window.ShowAsync();
            likerAnimator.SetTrigger(StartLikingTriggerName);
        }

        public void Hide()
        {
            window.SetVisibilityAbruptly(false);
        }
    }
}
