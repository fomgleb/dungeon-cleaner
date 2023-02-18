using Game.Scripts.Camera;
using Game.Scripts.Dungeon.Weapon;
using UnityEngine;

namespace Game.Scripts.Dungeon.Living_Entities.Player
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
