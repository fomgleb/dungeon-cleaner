using UnityEngine;
using UnnamedGame.Weapon.Scripts;

namespace UnnamedGame.LivingEntities.Player.Scripts
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private WeaponInput[] existingWeapons;
        [SerializeField] private WeaponInput currentWeapon;

        public WeaponInput CurrentWeapon => currentWeapon;

        private PlayerInput _playerInput;
    
        private void OnEnable() => _playerInput.AttackButtonClickedEvent += OnAttackButtonClicked;

        private void OnDisable() => _playerInput.AttackButtonClickedEvent -= OnAttackButtonClicked;
    
        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        private void Start()
        {
            foreach (var existingWeapon in existingWeapons)
                existingWeapon.gameObject.SetActive(false);
            currentWeapon.gameObject.SetActive(true);
        }

        private void OnAttackButtonClicked() => currentWeapon.SendAttackRequest();
    }
}