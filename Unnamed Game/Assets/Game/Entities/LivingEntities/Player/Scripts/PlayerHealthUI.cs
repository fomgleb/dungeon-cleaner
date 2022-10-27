using Game.Entities.LivingEntities.Scripts;
using TMPro;
using UnityEngine;
using UnnamedGame.LivingEntities.Player.Scripts;
using Zenject;

namespace Game.LivingEntities.Player.Scripts
{
    public class PlayerHealthUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text healthText;

        [Inject] private PlayerInput playerInput;

        private Damageable playerDamageable;
    
        private void Awake()
        {
            playerDamageable = playerInput.GetComponent<Damageable>();
        }

        private void Start()
        {
            SetHealthText();
        }

        private void OnEnable()
        {
            playerDamageable.GotDamageEvent += OnGotDamage;
        }

        private void OnDisable()
        {
            playerDamageable.GotDamageEvent -= OnGotDamage;
        }

        private void OnGotDamage() => SetHealthText();

        private void SetHealthText()
        {
            healthText.text = $"{playerDamageable.Health} / {playerDamageable.MaxHealth}";
        }
    }
}
