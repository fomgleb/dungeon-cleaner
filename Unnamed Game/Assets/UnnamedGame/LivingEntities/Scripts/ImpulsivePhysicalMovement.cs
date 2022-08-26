using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UnnamedGame.LivingEntities.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class ImpulsivePhysicalMovement : MonoBehaviour
    {
        [Header(nameof(ImpulsivePhysicalMovement))]
        [SerializeField] private float force;
        [SerializeField] private float reloadTime;

        private Rigidbody2D _rigidbody2D;
        protected bool IsCharged;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        protected async UniTask ReloadAsync()
        {
            
        }
        
        protected void DoImpulse(Vector2 direction)
        {
            _rigidbody2D.AddForce(force * direction, ForceMode2D.Impulse);
        }
    }
}