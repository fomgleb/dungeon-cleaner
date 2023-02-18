using UnityEngine;

namespace Game.Scripts.Dungeon.Weapon
{
    [RequireComponent(typeof(WeaponAttack))]
    public class WeaponAnimation : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private static readonly int Attack = Animator.StringToHash("Attack");
        
        private WeaponAttack weaponAttack;
        
        private void Awake()
        {
            weaponAttack = GetComponent<WeaponAttack>();
        }

        private void OnEnable()
        {
            weaponAttack.AttackedEvent += OnAttacked;
        }

        private void OnDisable()
        {
            weaponAttack.AttackedEvent -= OnAttacked;
        }

        private void OnAttacked() => AnimateAttacking();

        private void AnimateAttacking()
        {
            animator.SetTrigger(Attack);
        }
    }
}
