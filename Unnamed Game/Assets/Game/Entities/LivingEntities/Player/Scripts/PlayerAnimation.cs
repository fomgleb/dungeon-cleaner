using UnityEngine;
using UnnamedGame.LivingEntities.Player.Scripts;
using UnnamedGame.Mouse.Scripts;
using UnnamedGame.Pause;
using Zenject;

namespace Game.LivingEntities.Player.Scripts
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimation : MonoBehaviour, IPauseHandler
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        [Inject] private MouseFollower mouseFollower;
        [Inject] private Pauser pauser;
        
        private PlayerInput playerInput;
        private Animator animator;
         
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
            animator = GetComponent<Animator>();
            playerInput = GetComponent<PlayerInput>();
        }

        private void Update()
        {
            if (pauser.IsPaused)
                return;
            spriteRenderer.flipX = mouseFollower.transform.position.x < transform.position.x;
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
