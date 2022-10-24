using UnityEngine;

namespace UnnamedGame.LivingEntities.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class ImpulsivePhysicalMovement : MonoBehaviour
    {
        [SerializeField] private float force;

        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        
        protected void DoImpulse(Vector2 direction)
        {
            _rigidbody2D.AddForce(force * direction, ForceMode2D.Impulse);
        }
    }
}