using Game.Scripts.Pause;
using UnityEngine;

namespace Game.Scripts.Dungeon.Torches
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
