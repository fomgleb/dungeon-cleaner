using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Lean.Pool;
using UnityEngine;

namespace Game.Scripts.Game_Object
{
    public class GameObjectDestroyer : MonoBehaviour
    {
        [SerializeField] private float delayBeforeDestroying;
    
        private void OnEnable()
        {
            DespawnAsync();
        }

        private void OnDestroy()
        {
            cancelDespawnToken.Cancel();
        }

        private readonly CancellationTokenSource cancelDespawnToken = new();
        private async void DespawnAsync()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delayBeforeDestroying));
            if (cancelDespawnToken.IsCancellationRequested) return;
            LeanPool.Despawn(gameObject);
        }
    }
}
