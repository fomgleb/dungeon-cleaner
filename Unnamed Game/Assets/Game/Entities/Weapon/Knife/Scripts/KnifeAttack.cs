using System;
using Game.Entities.LivingEntities.Scripts;
using UnityEngine;
using UnnamedGame.Weapon.Scripts;

namespace Game.Entities.Weapon.Knife.Scripts
{
    [RequireComponent(typeof(WeaponAttack))]
    public class KnifeAttack : MonoBehaviour, IWeaponAttack
    {
        [SerializeField] private Transform attackPoint;
        public float attackRange;
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private LayerMask wallLayer;
        [SerializeField] private uint maxHitObjectsAtOnce;
        
        public event Action HitEnemyEvent;
        public event Action HitWallEvent;

        private WeaponAttack weaponAttack;
        
        private void Awake()
        {
            weaponAttack = GetComponent<WeaponAttack>();
            detectedColliders = new Collider2D[maxHitObjectsAtOnce];
        }

        private Collider2D[] detectedColliders;
        public void Attack()
        {
            var size = Physics2D.OverlapCircleNonAlloc(attackPoint.position, attackRange, detectedColliders, enemyLayer | wallLayer);
            for (var i = 0; i < size; i++)
            {
                var gameObjectLayerMask = 1 << detectedColliders[i].gameObject.layer;  
                if (gameObjectLayerMask == enemyLayer.value)
                {
                    detectedColliders[i].GetComponent<Damageable>().AddToHealth(transform, -weaponAttack.Damage);
                    HitEnemyEvent?.Invoke();
                }
                else if (gameObjectLayerMask == wallLayer.value)
                {
                    HitWallEvent?.Invoke();
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}