using UnityEngine;

namespace Game.Scripts.Dungeon.Living_Entities.Player
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
