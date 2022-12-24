using Game.Entities.LivingEntities.Scripts;
using TMPro;
using UnityEngine;

namespace Game.Entities.LivingEntities.Player.Scripts
{
    public class HealthHUD : MonoBehaviour
    {
        [SerializeField] private TMP_Text healthText;
        [SerializeField] private Damageable damageable;

        private void Start()
        {
            SetHealthText();
        }

        private void OnEnable()
        {
            damageable.HealthChangedEvent += OnHealthChanged;
        }

        private void OnDisable()
        {
            damageable.HealthChangedEvent -= OnHealthChanged;
        }

        private void OnHealthChanged(object sender, Damageable.HealthChangedEventArgs healthChangedEventArgs)
        {
            if (healthChangedEventArgs.AddedHealth != 0)
                SetHealthText();
        }
            
        private void SetHealthText()
        {
            healthText.text = $"{damageable.Health} / {damageable.MaxHealth}";
        }
    }
}
