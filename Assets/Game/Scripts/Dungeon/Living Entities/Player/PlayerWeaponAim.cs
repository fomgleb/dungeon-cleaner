using UnityEngine;

namespace Game.Entities.LivingEntities.Player.Scripts
{
    public class PlayerWeaponAim : MonoBehaviour
    {
        [SerializeField] private Collider2D playerCollider;

        private void Start()
        {
            transform.position = playerCollider.bounds.center;
        }
    }
}
