using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UnnamedGame.Weapon.Scripts
{
    [RequireComponent(typeof(WeaponInput))]
    public class WeaponAttack : MonoBehaviour
    {
        [SerializeField] private float reloadTime;

        public event Action AttackedEvent; 

        private UniTask _reloadTask;

        private WeaponInput _weaponInput;
        private IWeaponAttack _weaponAttack;

        private void Awake()
        {
            _weaponInput = GetComponent<WeaponInput>();
            _weaponAttack = GetComponent<IWeaponAttack>();
        }

        private void OnEnable() => _weaponInput.UserSentAttackRequestEvent += OnUserSentAttackRequest;
        private void OnDisable() => _weaponInput.UserSentAttackRequestEvent -= OnUserSentAttackRequest;

        private void OnUserSentAttackRequest() => TryMakeAttack();

        private void TryMakeAttack()
        {
            if (_reloadTask.Status != UniTaskStatus.Succeeded) return;
            _weaponAttack.Attack();
            AttackedEvent?.Invoke();
            _reloadTask = UniTask.Delay((int)(reloadTime * 1000));
        }
    }
}