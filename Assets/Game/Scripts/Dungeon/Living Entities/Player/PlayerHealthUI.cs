using TMPro;
using UnityEngine;

namespace Game.Scripts.Dungeon.Living_Entities.Player
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
