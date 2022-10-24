using UnityEngine;

namespace UnnamedGame.Weapon.Scripts
{
    [RequireComponent(typeof(WeaponAttack))]
    public class WeaponAnimation : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private static readonly int Attack = Animator.StringToHash("Attack");
        
        private WeaponAttack _weaponAttack;
        
        private void Awake()
        {
            _weaponAttack = GetComponent<WeaponAttack>();
        }

        private void OnEnable()
        {
            _weaponAttack.AttackedEvent += OnAttacked;
        }

        private void OnDisable()
        {
            _weaponAttack.AttackedEvent -= OnAttacked;
        }

        private void OnAttacked() => AnimateAttacking();

        private void AnimateAttacking()
        {
            animator.SetTrigger(Attack);
        }
    }
}
