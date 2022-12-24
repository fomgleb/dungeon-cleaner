using Game.Architecture.Camera.Scripts;
using UnityEngine;
using Zenject;

namespace UnnamedGame.Camera.Scripts
{
    public class CameraShakerInstaller : MonoInstaller
    {
        [SerializeField] private CameraShaker cameraShaker;
        
        public override void InstallBindings()
        {
            Container
                .Bind<CameraShaker>()
                .FromInstance(cameraShaker)
                .AsSingle();
        }
    }
}