using Game.Entities.LivingEntities.Scripts;
using UnityEngine;

namespace Game.Entities.LivingEntities.Player.Scripts
{
    [RequireComponent(typeof(LinearPhysicalMovement))]
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerMovement : MonoBehaviour
    {
        private LinearPhysicalMovement linearPhysicalMovement;
        private PlayerInput playerInput;

        private void Awake()
        {
            linearPhysicalMovement = GetComponent<LinearPhysicalMovement>();
            playerInput = GetComponent<PlayerInput>();
        }

        private void FixedUpdate()
        {
            if (playerInput.EnteredMovementDirection != Vector2.zero)
                linearPhysicalMovement.Move(playerInput.EnteredMovementDirection);
        }
    }
}
