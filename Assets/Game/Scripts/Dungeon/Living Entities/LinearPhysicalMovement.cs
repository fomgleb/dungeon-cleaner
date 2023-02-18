using UnityEngine;

namespace Game.Scripts.Dungeon.Living_Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class LinearPhysicalMovement : MonoBehaviour
    {
        [SerializeField] private float speed;

        private new Rigidbody2D rigidbody2D;

        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }
        
        public void Move(Vector2 direction)
        {
            rigidbody2D.AddForce(speed * 100 * direction);
        }
    }
}
