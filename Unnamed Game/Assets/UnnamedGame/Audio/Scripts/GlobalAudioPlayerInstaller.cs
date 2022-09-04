using UnityEngine;
using Zenject;

namespace UnnamedGame.Audio.Scripts
{
    public class GlobalAudioPlayerInstaller : MonoInstaller
    {
        [SerializeField] private GlobalAudioPlayer globalAudioPlayer;

        public override void InstallBindings()
        {
            Container
                .Bind<GlobalAudioPlayer>()
                .FromInstance(globalAudioPlayer)
                .AsSingle();
        }
    }
}
