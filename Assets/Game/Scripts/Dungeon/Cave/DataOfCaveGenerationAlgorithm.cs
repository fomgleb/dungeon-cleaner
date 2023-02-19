using UnityEngine;

namespace Game.Scripts.Dungeon.Cave
{
    [CreateAssetMenu(menuName = "My Assets/Dungeon Generation Data", fileName = "DungeonGenerationData", order = 0)]
    public class DataOfCaveGenerationAlgorithm : ScriptableObject
    {
        [SerializeField] private Vector2Int startPosition;
        public Vector2Int StartPosition => startPosition;
        [SerializeField] private uint stepsForOneDirection = 10;
        public uint StepsForOneDirection => stepsForOneDirection;
        [SerializeField] private uint numberOfSteps = 200;
        public uint NumberOfSteps => numberOfSteps;
        [SerializeField] [Range(0, 1)] private float chanceToTurn = 0.5f;
        public float ChanceToTurn => chanceToTurn;
    }
}
