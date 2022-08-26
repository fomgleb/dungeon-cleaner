using UnityEngine;
using UnnamedGame.Mouse.Scripts;

namespace UnnamedGame.LivingEntities.Player.Scripts
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private MouseFollower mouseFollower;
        
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private new Camera camera;
        
        private PlayerInput _playerInput;
        
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        private void OnEnable()
        {
            _playerInput.EnteredMovementDirectionChangedEvent += OnMovementDirectionChanged;
        }

        private void OnDisable()
        {
            _playerInput.EnteredMovementDirectionChangedEvent -= OnMovementDirectionChanged;
        }
        
        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        private void Update()
        {
            spriteRenderer.flipX = mouseFollower.transform.position.x < transform.position.x;
        }
        
        private void OnMovementDirectionChanged(Vector2 enteredMovementDirection)
        {
            animator.SetBool(IsMoving, enteredMovementDirection != Vector2.zero);
        }
    }
}
