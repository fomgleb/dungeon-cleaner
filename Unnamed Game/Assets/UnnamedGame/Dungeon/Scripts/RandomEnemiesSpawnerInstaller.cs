using UnityEngine;
using Zenject;

namespace UnnamedGame.Dungeon.Scripts
{
    public class RandomEnemiesSpawnerInstaller : MonoInstaller
    {
        [SerializeField] private RandomEnemiesSpawner randomEnemiesSpawner;
        
        public override void InstallBindings()
        {
            Container
                .Bind<RandomEnemiesSpawner>()
                .FromInstance(randomEnemiesSpawner)
                .AsSingle();
        }
    }
}