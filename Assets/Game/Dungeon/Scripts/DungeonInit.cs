using Game.Pause;
using UnityEngine;

namespace Game.Dungeon.Scripts
{
    public class DungeonInit : MonoBehaviour
    {
        [SerializeField] private GameObjectSpawner playerSpawner;

        private void Start()
        {
            Pauser.ClearRegisteredHandlers();
            playerSpawner.Spawn();
        }
    }
}
