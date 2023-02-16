using Game.Entities.Weapon.Scripts;
using UnityEngine;

namespace Game.Entities.LivingEntities.Player.Scripts
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private WeaponInput[] existingWeapons;
        [SerializeField] private WeaponInput currentWeapon;

        public WeaponInput CurrentWeapon => currentWeapon;

        private PlayerInput playerInput;
    
        private void OnEnable() => playerInput.AttackButtonClickedEvent += OnAttackButtonClicked;

        private void OnDisable() => playerInput.AttackButtonClickedEvent -= OnAttackButtonClicked;
    
        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
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
