using System;
using Game.Pause;
using UnityEngine;
using Zenject;

namespace Game.Mouse.Scripts
{
    public class RotatorToMouse : MonoBehaviour
    {
        private void Update()
        {
            if (Pauser.IsPaused)
                return;
            
            Rotate();
        }

        private void Rotate()
        {
            var aimDirection = (MouseLocation.WorldPosition - (Vector2)transform.position).normalized;
            var angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle);
            transform.localScale = angle is > 90 or < -90 ? new Vector3(1, -1, 1) : new Vector3(1, 1, 1);
        }
    }
}
