using Game.Mouse.Scripts;
using Game.Pause;
using UnityEngine;

namespace Game.Entities.LivingEntities.Player.Scripts
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimation : MonoBehaviour, IPauseHandler
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        private PlayerInput playerInput;
        private Animator animator;

        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        private void OnEnable()
        {
            playerInput.EnteredMovementDirectionChangedEvent += OnMovementDirectionChanged;
            Pauser.Register(this);
        }

        private void OnDisable()
        {
            playerInput.EnteredMovementDirectionChangedEvent -= OnMovementDirectionChanged;
            Pauser.UnRegister(this);
        }
        
        private void Awake()
        {
            animator = GetComponent<Animator>();
            playerInput = GetComponent<PlayerInput>();
        }

        private void Update()
        {
            if (Pauser.IsPaused)
                return;
            spriteRenderer.flipX = MouseLocation.WorldPosition.x < transform.position.x;
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
