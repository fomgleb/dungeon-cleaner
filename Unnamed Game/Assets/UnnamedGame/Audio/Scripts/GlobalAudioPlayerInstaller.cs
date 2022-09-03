using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnnamedGame.Audio.Scripts;
using Zenject;

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
