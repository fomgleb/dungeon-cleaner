using Game.Camera.Scripts;
using Game.Entities.Weapon.Scripts;
using UnityEngine;
using Zenject;

namespace Game.Entities.LivingEntities.Player.Scripts
{
    [RequireComponent(typeof(PlayerAttack))]
    public class PlayerCameraShaking : MonoBehaviour
    {
        [SerializeField] private float intensity;
        [SerializeField] private float time;

        [Inject] private CameraShaker cameraShaker;
        
        private PlayerAttack playerAttack;
        private WeaponAttack weaponAttack;
        
        private void Awake()
        {
            playerAttack = GetComponent<PlayerAttack>();
            weaponAttack = playerAttack.CurrentWeapon.GetComponent<WeaponAttack>();
        }

        private void OnEnable()
        {
            weaponAttack.AttackedEvent += OnAttacked;
        }

        private void OnDisable()
        {
            weaponAttack.AttackedEvent -= OnAttacked;
        }

        private void OnAttacked() => cameraShaker.ShakeCameraAsync(intensity, time);
    }
}
