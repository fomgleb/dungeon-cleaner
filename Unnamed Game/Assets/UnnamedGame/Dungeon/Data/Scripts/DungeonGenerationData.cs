using UnityEngine;

namespace UnnamedGame.Dungeon.Data.Scripts
{
    [CreateAssetMenu(menuName = "My Assets/Dungeon Generation Data", fileName = "DungeonGenerationData", order = 0)]
    public class DungeonGenerationData : ScriptableObject
    {
        [SerializeField] private int stepsForOneDirection = 10;
        public int StepsForOneDirection => stepsForOneDirection;
        [SerializeField] private int numberOfSteps = 200;
        public int NumberOfSteps => numberOfSteps;
        [SerializeField] [Range(0, 1)] private float chanceToTurn = 0.5f;
        public float ChanceToTurn => chanceToTurn;
    }
}
