using UnityEngine;

namespace Game.Scripts.Dungeon.Living_Entities.Slime
{
    [RequireComponent(typeof(SlimeAI))]
    [RequireComponent(typeof(SlimeMovement))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class SlimeAttack : MonoBehaviour
    {
        [SerializeField] private float damage;
        [Tooltip("The damage will be performed only if the slime speed is greater or equal than this.")]
        [SerializeField] private float attackSpeed;

        private SlimeAI slimeAI;
        private SlimeMovement slimeMovement;
        private new Rigidbody2D rigidbody2D;

        private Damageable targetDamageable;
        private Damageable TargetDamageable
        {
            get
            {
                if (targetDamageable != null) return targetDamageable;
                if (slimeAI.TargetGameObject == null) return null;
                
                targetDamageable = slimeAI.TargetGameObject.GetComponent<Damageable>();
                return targetDamageable;
            }
        }
        
        private Rigidbody2D targetRigidbody2D;
        private Rigidbody2D TargetRigidbody2D
        {
            get
            {
                if (targetRigidbody2D != null) return targetRigidbody2D;
                if (slimeAI.TargetGameObject == null) return null;
                
                targetRigidbody2D = slimeAI.TargetGameObject.GetComponent<Rigidbody2D>();
                return targetRigidbody2D;
            }
        }
        
        private void Awake()
        {
            slimeAI = GetComponent<SlimeAI>();
            slimeMovement = GetComponent<SlimeMovement>();
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            slimeMovement.TouchedOtherColliderEvent += OnTouchedOtherCollider;
        }

        private void OnDisable()
        {
            slimeMovement.TouchedOtherColliderEvent -= OnTouchedOtherCollider;
        }

        private void OnTouchedOtherCollider(Collision2D collision)
        {
            if (TargetDamageable == null) return;
            if (collision.gameObject.CompareTag(slimeAI.TargetCollider.tag))
                TryPerformDamageToTarget();
        }

        private void TryPerformDamageToTarget()
        {
            var targetVelocity = TargetRigidbody2D.velocity;
            var slimeVelocity = rigidbody2D.velocity;
            if ((targetVelocity - slimeVelocity).magnitude >= attackSpeed)
                TargetDamageable.AddToHealth(transform, -damage);
        }
    }
}
