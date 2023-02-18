using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Scripts.Dungeon.Weapon
{
    [RequireComponent(typeof(WeaponInput))]
    public class WeaponAttack : MonoBehaviour
    {
        [SerializeField] private float reloadTime;
        [SerializeField] private float damage;

        public float Damage => damage;
        
        public event Action AttackedEvent; 

        private UniTask reloadTask;

        private WeaponInput weaponInput;
        private IWeaponAttack weaponAttack;

        private void Awake()
        {
            weaponInput = GetComponent<WeaponInput>();
            weaponAttack = GetComponent<IWeaponAttack>();
        }

        private void OnEnable() => weaponInput.UserSentAttackRequestEvent += OnUserSentAttackRequest;
        private void OnDisable() => weaponInput.UserSentAttackRequestEvent -= OnUserSentAttackRequest;

        private void OnUserSentAttackRequest() => TryMakeAttack();

        private void TryMakeAttack()
        {
            if (reloadTask.Status != UniTaskStatus.Succeeded) return;
            weaponAttack.Attack();
            AttackedEvent?.Invoke();
            reloadTask = UniTask.Delay((int)(reloadTime * 1000));
        }
    }
}
