using UnityEngine;

namespace Game.Scripts.Dungeon.Living_Entities.Player
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
