using UnityEngine;

namespace Game.Entities.LivingEntities.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class ImpulsivePhysicalMovement : MonoBehaviour
    {
        [SerializeField] private float force;

        private new Rigidbody2D rigidbody2D;

        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }
        
        protected void DoImpulse(Vector2 direction)
        {
            rigidbody2D.AddForce(force * direction, ForceMode2D.Impulse);
        }
    }
}
