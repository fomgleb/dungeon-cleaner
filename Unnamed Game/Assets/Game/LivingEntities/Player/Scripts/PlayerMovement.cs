using UnityEngine;
using UnnamedGame.LivingEntities.Scripts;

namespace UnnamedGame.LivingEntities.Player.Scripts
{
    [RequireComponent(typeof(LinearPhysicalMovement))]
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerMovement : MonoBehaviour
    {
        private LinearPhysicalMovement _linearPhysicalMovement;
        private PlayerInput _playerInput;

        private void Awake()
        {
            _linearPhysicalMovement = GetComponent<LinearPhysicalMovement>();
            _playerInput = GetComponent<PlayerInput>();
        }

        private void FixedUpdate()
        {
            if (_playerInput.EnteredMovementDirection != Vector2.zero)
                _linearPhysicalMovement.Move(_playerInput.EnteredMovementDirection);
        }
    }
}
