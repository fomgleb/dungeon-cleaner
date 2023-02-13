using UnityEngine;
using Zenject;

namespace Game.Dungeon.Scripts
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
