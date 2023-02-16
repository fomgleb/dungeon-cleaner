using Game.Entities.LivingEntities.Scripts;
using TMPro;
using UnityEngine;

namespace Game.Entities.LivingEntities.Player.Scripts
{
    public class PlayerHealthUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text healthText;

        private Damageable playerDamageable;
    
        public void Init(Damageable playerDamageable)
        {
            this.playerDamageable = playerDamageable;
        }

        public void SetHealthText()
        {
            healthText.text = $"{playerDamageable.Health} / {playerDamageable.MaxHealth}";
        }
    }
}
