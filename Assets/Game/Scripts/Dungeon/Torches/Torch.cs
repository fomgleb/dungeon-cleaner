using Game.Pause;
using UnityEngine;

namespace Game.Dungeon.Torches.Scripts
{
    public class Torch : MonoBehaviour, IPauseHandler
    {
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            Pauser.Register(this);
        }

        void IPauseHandler.SetPaused(bool isPaused)
        {
            animator.speed = Pauser.IsPaused ? 0 : 1;
        }
    }
}
