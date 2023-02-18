using UnityEngine;

namespace Game.Scripts.Dungeon.Healing_Potion
{
    public class HealingPotionData : ScriptableObject
    {
        [SerializeField] private float healingAmount;
        [SerializeField] private Sprite potionSprite;

        public float HealingAmount => healingAmount;
        public Sprite PotionSprite => potionSprite;
    }
}