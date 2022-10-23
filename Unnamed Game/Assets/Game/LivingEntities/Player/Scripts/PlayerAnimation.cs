using UnityEngine;
using UnnamedGame.Mouse.Scripts;
using UnnamedGame.Pause;
using Zenject;

namespace UnnamedGame.LivingEntities.Player.Scripts
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerAnimation : MonoBehaviour, IPauseHandler
    {
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer spriteRenderer;

        [Inject] private MouseFollower mouseFollower;
        [Inject] private Pauser pauser;
        
        private PlayerInput playerInput;
         
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        private void OnEnable()
        {
            playerInput.EnteredMovementDirectionChangedEvent += OnMovementDirectionChanged;
            pauser.Register(this);
        }

        private void OnDisable()
        {
            playerInput.EnteredMovementDirectionChangedEvent -= OnMovementDirectionChanged;
            pauser.UnRegister(this);
        }
        
        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
        }

        private void Update()
        {
            if (pauser.IsPaused)
                return;
            if (Time.timeScale != 0)
            {
                spriteRenderer.flipX = mouseFollower.transform.position.x < transform.position.x;
            }
        }
        
        private void OnMovementDirectionChanged(Vector2 enteredMovementDirection)
        {
            animator.SetBool(IsMoving, enteredMovementDirection != Vector2.zero);
        }

        void IPauseHandler.SetPaused(bool isPaused)
        {
            animator.speed = isPaused ? 0 : 1;
        }
    }
}
