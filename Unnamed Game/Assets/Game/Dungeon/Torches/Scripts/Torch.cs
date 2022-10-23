using UnityEngine;
using UnnamedGame.Pause;
using Zenject;

namespace UnnamedGame.Dungeon.Torches.Scripts
{
    public class Torch : MonoBehaviour, IPauseHandler
    {
        [Inject] private Pauser pauser;

        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            pauser.Register(this);
        }

        void IPauseHandler.SetPaused(bool isPaused)
        {
            animator.speed = pauser.IsPaused ? 0 : 1;
        }
    }
}
