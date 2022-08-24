using UnityEngine;

namespace UnnamedGame.Dungeon.Scripts
{
    [RequireComponent(typeof(DungeonTilemapVisualizer))]
    public abstract class DungeonGeneratorBase : MonoBehaviour
    {
        [Header("Dungeon Generator Base")]
        [SerializeField] protected DungeonTilemapVisualizer dungeonTilemapVisualizer = null;
        [SerializeField] protected Vector2Int startPosition = Vector2Int.zero;

        public void _GenerateDungeon()
        {
            dungeonTilemapVisualizer.Clear();
            RunProceduralGeneration();
        }

        protected abstract void RunProceduralGeneration();
    }
}
