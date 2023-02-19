using Game.Scripts.Game_Object;
using UnityEngine;

namespace Game.Scripts.Dungeon.Living_Entities.Player
{
    [RequireComponent(typeof(GameObjectSpawner))]
    public class PlayerSpawner : MonoBehaviour
    {
        private GameObjectSpawner gameObjectSpawner;

        public GameObjectSpawner GameObjectSpawner => gameObjectSpawner;

        private void Awake()
        {
            gameObjectSpawner = GetComponent<GameObjectSpawner>();
        }

        public void Spawn()
        {
            
        }
    }
}
