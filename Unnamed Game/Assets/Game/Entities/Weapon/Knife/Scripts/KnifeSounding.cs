using Game.Audio.Scripts;
using UnityEngine;

namespace Game.Entities.Weapon.Knife.Scripts
{
    [RequireComponent(typeof(KnifeAttack))]
    public class KnifeSounding : MonoBehaviour
    {
        [SerializeField] private Sound damageSlimeSound;

        private KnifeAttack knifeAttack;
        
        private void Awake()
        {
            knifeAttack = GetComponent<KnifeAttack>();
        }

        private void OnEnable()
        {
            knifeAttack.HitEnemyEvent += OnHitEnemy;
        }

        private void OnDisable()
        {
            knifeAttack.HitEnemyEvent -= OnHitEnemy;
        }

        private void OnHitEnemy()
        {
            damageSlimeSound.Play();
        }
    }
}