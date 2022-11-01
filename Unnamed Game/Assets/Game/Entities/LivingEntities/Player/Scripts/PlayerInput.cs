using System;
using Game.Pause;
using UnityEngine;
using UnnamedGame.Pause;
using Zenject;

namespace UnnamedGame.LivingEntities.Player.Scripts
{
    public class PlayerInput : MonoBehaviour
    {
        public Vector2 EnteredMovementDirection { get; private set; }

        public event Action<Vector2> EnteredMovementDirectionChangedEvent;
        public event Action AttackButtonClickedEvent;

        [Inject] private Pauser pauser;

        private void Update()
        {
            if (pauser.IsPaused)
                return;
            var storedDirection = EnteredMovementDirection;
            EnteredMovementDirection = GetEnteredMovementDirection();
            if (EnteredMovementDirection != storedDirection)
                EnteredMovementDirectionChangedEvent?.Invoke(EnteredMovementDirection);
            if (Input.GetButtonDown("Fire1"))
                AttackButtonClickedEvent?.Invoke();
        }

        private static Vector2 GetEnteredMovementDirection()
        {
            return new Vector2()
            {
                x = Input.GetAxisRaw("Horizontal"),
                y = Input.GetAxisRaw("Vertical")
            }.normalized;
        }
    }
}
