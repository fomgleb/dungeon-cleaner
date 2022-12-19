using UnityEngine;
using UnnamedGame.LivingEntities.Player.Scripts;
using UnnamedGame.Weapon.Scripts;

namespace Game.LivingEntities.Player.Scripts
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