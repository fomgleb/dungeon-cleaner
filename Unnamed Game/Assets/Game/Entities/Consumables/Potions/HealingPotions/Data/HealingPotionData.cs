using UnityEngine;

namespace Game.Entities.Consumables.Potions.HealingPotions.Data
{
    public class HealingPotionData : ScriptableObject
    {
        [SerializeField] private float healingAmount;
        [SerializeField] private Sprite potionSprite;

        public float HealingAmount => healingAmount;
        public Sprite PotionSprite => potionSprite;
    }
}