using System;
using Cysharp.Threading.Tasks;
using Lean.Pool;
using UnityEngine;

namespace Game.Scripts
{
    public class GameObjectDestroyer : MonoBehaviour
    {
        [SerializeField] private float delayBeforeDestroying;
    
        private void OnEnable()
        {
            DespawnAsync();
        }

        private async void DespawnAsync()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delayBeforeDestroying));
            if (gameObject != null)
                LeanPool.Despawn(gameObject);
        }
    }
}
