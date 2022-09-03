using TMPro;
using UnityEngine;
using UnnamedGame.LivingEntities.Scripts;
using Zenject;

namespace UnnamedGame.LivingEntities.Player.Scripts
{
    public class PlayerHealthUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text healthText;

        [Inject] private PlayerInput _playerInput;

        private Damageable _playerDamageable;
    
        private void Awake()
        {
            _playerDamageable = _playerInput.GetComponent<Damageable>();
        }

        private void OnEnable()
        {
            _playerDamageable.GotDamageEvent += OnGotDamage;
        }

        private void OnDisable()
        {
            _playerDamageable.GotDamageEvent -= OnGotDamage;
        }

        private void OnGotDamage() => healthText.text = $"{_playerDamageable.Health} / {_playerDamageable.MaxHealth}";
    }
}
