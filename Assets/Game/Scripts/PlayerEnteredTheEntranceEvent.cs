using Game.Scripts.Game_Object;
using UnityEngine;

namespace Game.Scripts
{
    public class PlayerEnteredTheEntranceEvent : MonoBehaviour
    {
        [SerializeField] private GameObjectSpawner entranceSpawner;

        private void Start()
        {
            entranceSpawner.SpawnedEvent += () =>
                entranceSpawner.SpawnedObject.GetComponent<Entrance>().CollidedWithPlayerEvent += OnEntranceCollidedWithPlayer;;
        }

        private void OnEntranceCollidedWithPlayer()
        {
            Debug.Log("Player wanna get out of here.");
        }
    }
}
