using System;
using UnityEngine;

namespace Game.Scripts
{
    [RequireComponent(typeof(Collider2D))]
    public class Entrance : MonoBehaviour
    {
        public event Action CollidedWithPlayerEvent;

        [SerializeField] private GameObject playerPrefabWithTag;
        
        private GameObject playerGameObject;

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag(playerPrefabWithTag.tag))
                CollidedWithPlayerEvent?.Invoke();
        }
    }
}