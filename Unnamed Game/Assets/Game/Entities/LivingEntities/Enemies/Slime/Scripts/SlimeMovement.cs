using System;
using Game.Entities.LivingEntities.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Entities.LivingEntities.Enemies.Slime.Scripts
{
    [RequireComponent(typeof(SlimeAI))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Damageable))]
    public class SlimeMovement : MonoBehaviour
    {
        [SerializeField] private float minForce;
        [SerializeField] private float maxForce;
        
        private SlimeAI slimeAI;
        private new Rigidbody2D rigidbody2D;
        private Damageable damageable;

        public event Action<Collision2D> TouchedOtherColliderEvent;

        private void Awake()
        {
            slimeAI = GetComponent<SlimeAI>();
            rigidbody2D = GetComponent<Rigidbody2D>();
            damageable = GetComponent<Damageable>();
        }

        private void OnEnable()
        {
            slimeAI.ReadyToMakeImpulseEvent += OnReadyToMakeImpulse;
            damageable.HealthChangedEvent += OnHealthChanged;
        }

        private void OnDisable()
        {
            slimeAI.ReadyToMakeImpulseEvent -= OnReadyToMakeImpulse;
            damageable.HealthChangedEvent += OnHealthChanged;
        }

        private void OnHealthChanged(object sender, Damageable.HealthChangedEventArgs e)
        {
            rigidbody2D.AddForce((transform.position - e.HealthChanger.transform.position) * 30000);
        }

        private void OnReadyToMakeImpulse(object sender, SlimeAI.ReadyToMakeImpulseEventArgs e)
        {
            rigidbody2D.AddForce(e.Direction * Random.Range(minForce, maxForce) * 100, ForceMode2D.Impulse);
        }
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            TouchedOtherColliderEvent?.Invoke(col);
        }
    }
}