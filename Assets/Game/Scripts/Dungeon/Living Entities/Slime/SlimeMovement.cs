using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Dungeon.Living_Entities.Slime
{
    [RequireComponent(typeof(SlimeAI))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class SlimeMovement : MonoBehaviour
    {
        [SerializeField] private float minForce;
        [SerializeField] private float maxForce;
        
        private SlimeAI slimeAI;
        private new Rigidbody2D rigidbody2D;

        public event Action<Collision2D> TouchedOtherColliderEvent;

        private void Awake()
        {
            slimeAI = GetComponent<SlimeAI>();
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            slimeAI.ReadyToMakeImpulseEvent += OnReadyToMakeImpulse;
        }

        private void OnDisable()
        {
            slimeAI.ReadyToMakeImpulseEvent -= OnReadyToMakeImpulse;
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
