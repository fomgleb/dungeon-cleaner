using UnityEngine;

namespace Game.Entities.LivingEntities.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class LinearPhysicalMovement : MonoBehaviour
    {
        [SerializeField] private float speed;

        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        
        public void Move(Vector2 direction)
        {
            _rigidbody2D.AddForce(speed * 100 * direction);
        }
    }
}
