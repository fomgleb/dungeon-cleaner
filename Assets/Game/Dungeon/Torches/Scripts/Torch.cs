using Game.Pause;
using UnityEngine;
using Zenject;

namespace Game.Dungeon.Torches.Scripts
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
