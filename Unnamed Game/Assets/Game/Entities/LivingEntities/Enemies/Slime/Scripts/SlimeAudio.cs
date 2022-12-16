using System;
using Game.Audio.Scripts;
using Game.Entities.LivingEntities.Scripts;
using Game.LivingEntities.Enemies.Slime.Scripts;
using UnityEngine;

namespace Game.Entities.LivingEntities.Enemies.Slime.Scripts
{
    [RequireComponent(typeof(SlimeMovement))]
    [RequireComponent(typeof(Damageable))]
    public class SlimeAudio : MonoBehaviour
    {
        [SerializeField] private Sound hitTheWallSound;
        [SerializeField] private Sound hitOtherSlimeSound;
        [SerializeField] private LayerMask wallLayer;

        private SlimeMovement slimeMovement;
        private Damageable damageable;

        private void Awake()
        {
            slimeMovement = GetComponent<SlimeMovement>();
            damageable = GetComponent<Damageable>();
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
            var otherColliderLayer = 1 << collision.collider.gameObject.layer;
            if (otherColliderLayer == wallLayer)
            {
                hitTheWallSound.Play();
            }
            else if (otherColliderLayer == 1 << gameObject.layer)
            {
                hitOtherSlimeSound.Play();
            }
        }
    }
}