using UnityEngine;
using Zenject;

namespace Game.Camera.Scripts
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
