using System;
using UnityEngine;

namespace UnnamedGame.LivingEntities.Player.Scripts
{
    [RequireComponent(typeof(PlayerMovementController))]
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private new Camera camera;
        
        private PlayerMovementController _playerMovementController;
        
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        private void OnEnable()
        {
            _playerMovementController.EnteredMovementDirectionChangedEvent += OnEnteredMovementDirectionChanged;
        }

        private void OnDisable()
        {
            _playerMovementController.EnteredMovementDirectionChangedEvent -= OnEnteredMovementDirectionChanged;
        }

        private void Update()
        {
            spriteRenderer.flipX = camera.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x;
        }

        private void Awake()
        {
            _playerMovementController = GetComponent<PlayerMovementController>();
        }

        private void OnEnteredMovementDirectionChanged(Vector2 enteredMovementDirection)
        {
            animator.SetBool(IsMoving, enteredMovementDirection != Vector2.zero);
        }
    }
}
