using UnityEngine;
using Zenject;

namespace UnnamedGame.Mouse.Scripts
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
