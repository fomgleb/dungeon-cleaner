using Lean.Pool;
using UnityEngine;

namespace Game.Scripts
{
    public class GameObjectDestroyer : MonoBehaviour
    {
        [SerializeField] private float delayBeforeDestroying;
    
        private void Start()
        {
            LeanPool.Despawn(gameObject, delayBeforeDestroying);
        }
    }
}
