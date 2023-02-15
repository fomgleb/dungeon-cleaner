using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Game.Mouse.Scripts
{
    public class MouseFollowerInstaller : MonoInstaller
    {
        [FormerlySerializedAs("mouseFollower")] [FormerlySerializedAs("mouse")] [SerializeField] private MouseLocation mouseLocation;
        
        public override void InstallBindings()
        {
            Container
                .Bind<MouseLocation>()
                .FromInstance(mouseLocation)
                .AsSingle();
        }
    }
}
