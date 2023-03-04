using Game.Scripts.Dungeon;
using Game.Scripts.Game_Object;
using UnityEngine;

namespace Game.Scripts
{
    public class EntranceCollidedWithPlayerEvent : MonoBehaviour
    {
        [SerializeField] private GameObjectSpawner entranceSpawner;

        [SerializeField] private DungeonInit dungeonInit;

        private void Start()
        {
            entranceSpawner.SpawnedEvent += OnEntranceSpawned;
        }

        private void OnEntranceSpawned()
        {
            entranceSpawner.SpawnedObject.GetComponent<Entrance>().CollidedWithPlayerEvent += OnEntranceCollidedWithPlayer;
        }

        private void OnEntranceCollidedWithPlayer()
        {
            dungeonInit.Init();
        }
    }
}
