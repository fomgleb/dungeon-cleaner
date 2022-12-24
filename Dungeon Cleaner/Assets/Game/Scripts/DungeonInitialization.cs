using Game.Entities.Scripts;
using UnityEngine;

namespace Game.Scripts
{
    public class DungeonInitialization : MonoBehaviour
    {
        [SerializeField] private EntitySpawner playerSpawner;

        private void Start()
        {
            playerSpawner.Spawn();
        }
    }
}
