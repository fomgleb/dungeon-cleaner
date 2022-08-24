using System;
using UnityEngine;
using UnnamedGame.LivingEntities.Scripts;

namespace UnnamedGame.LivingEntities.Player.Scripts
{
    [RequireComponent(typeof(PhysicalMover))]
    public class PlayerMovementController : MonoBehaviour
    {
        private PhysicalMover _physicalMover;

        public Vector2 EnteredMovementDirection { get; private set; }

        public event Action<Vector2> EnteredMovementDirectionChangedEvent; 

        private void Awake()
        {
            _physicalMover = GetComponent<PhysicalMover>();
        }

        private void Update()
        {
            var storedDirection = EnteredMovementDirection;
            EnteredMovementDirection = GetTheEnteredMovementDirection();
            if (EnteredMovementDirection == Vector2.zero && storedDirection == Vector2.zero) return;
            EnteredMovementDirectionChangedEvent?.Invoke(EnteredMovementDirection);
        }

        private void FixedUpdate()
        {
            if (EnteredMovementDirection == Vector2.zero) return;
            _physicalMover.Move(EnteredMovementDirection);
        }

        private static Vector2 GetTheEnteredMovementDirection()
        {
            return new Vector2()
            {
                x = Input.GetAxisRaw("Horizontal"),
                y = Input.GetAxisRaw("Vertical")
            }.normalized;
        }
    }
}
