using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Pause;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

namespace Game.Dungeon.Torches.Scripts
{
    public class LightShaking : MonoBehaviour
    {
        [SerializeField] private float shakingOffset;
        [SerializeField] private float secondsBetweenShakes;
        
        private Light2D light2D;
        private float originPointLightOuterRadius;

        private void Awake()
        {
            light2D = GetComponent<Light2D>();
            stopShakingLightToken = new CancellationTokenSource();
        }

        private void Start()
        {
            originPointLightOuterRadius = light2D.pointLightOuterRadius;
            ShakeLightAsync();
        }

        private void OnDestroy()
        {
            stopShakingLightToken.Cancel();
        }

        private CancellationTokenSource stopShakingLightToken;

        private async void ShakeLightAsync()
        {
            while (true)
            {
                if (stopShakingLightToken.IsCancellationRequested)
                    return;
                await UniTask.Delay((int)(secondsBetweenShakes * 1000));
                if (Pauser.IsPaused)
                    continue;
                var newPointLightOuterRadius = Random.Range(originPointLightOuterRadius - shakingOffset, originPointLightOuterRadius + shakingOffset);
                light2D.pointLightOuterRadius = newPointLightOuterRadius;
            }
        }
    }
}
