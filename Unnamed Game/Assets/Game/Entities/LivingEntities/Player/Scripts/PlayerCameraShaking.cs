using Game.LivingEntities.Player.Scripts;
using UnityEngine;
using UnnamedGame.Camera.Scripts;
using UnnamedGame.Weapon.Scripts;
using Zenject;

namespace UnnamedGame.LivingEntities.Player.Scripts
{
    [RequireComponent(typeof(PlayerAttack))]
    public class PlayerCameraShaking : MonoBehaviour
    {
        [SerializeField] private float intensity;
        [SerializeField] private float time;

        [Inject] private CameraShaker _cameraShaker;
        
        private PlayerAttack _playerAttack;
        private WeaponAttack _weaponAttack;
        
        private void Awake()
        {
            _playerAttack = GetComponent<PlayerAttack>();
            _weaponAttack = _playerAttack.CurrentWeapon.GetComponent<WeaponAttack>();
        }

        private void OnEnable()
        {
            _weaponAttack.AttackedEvent += OnAttacked;
        }

        private void OnDisable()
        {
            _weaponAttack.AttackedEvent -= OnAttacked;
        }

        private void OnAttacked() => _cameraShaker.ShakeCameraAsync(intensity, time);
    }
}