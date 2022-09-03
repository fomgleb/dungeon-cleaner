using UnityEngine;
using UnnamedGame.Mouse.Scripts;
using Zenject;

namespace UnnamedGame.Mouse.Installers
{
    public class MouseFollowerInstaller : MonoInstaller
    {
        [SerializeField] private MouseFollower mouseFollower;
        
        public override void InstallBindings()
        {
            Container
                .Bind<MouseFollower>()
                .FromInstance(mouseFollower)
                .AsSingle();
        }
    }
}
