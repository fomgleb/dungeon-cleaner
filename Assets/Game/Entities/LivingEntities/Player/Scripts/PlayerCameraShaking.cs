using Game.Camera.Scripts;
using Game.Entities.Weapon.Scripts;
using UnityEngine;

namespace Game.Entities.LivingEntities.Player.Scripts
{
    [RequireComponent(typeof(PlayerAttack))]
    public class PlayerCameraShaking : MonoBehaviour
    {
        [SerializeField] private float intensity;
        [SerializeField] private float time;

        private PlayerAttack playerAttack;
        private WeaponAttack weaponAttack;

        private CameraShaker cameraShaker;
        
        private void Awake()
        {
            playerAttack = GetComponent<PlayerAttack>();
            weaponAttack = playerAttack.CurrentWeapon.GetComponent<WeaponAttack>();
            cameraShaker = GameObject.FindWithTag(nameof(CameraShaker)).GetComponent<CameraShaker>();
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
