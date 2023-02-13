using UnityEngine;
using Zenject;

namespace Game.Entities.LivingEntities.Player.Scripts
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerInput playerPrefab;
        [SerializeField] private Transform startPoint;
        
        public override void InstallBindings()
        {
            var playerInstance =
                Container.InstantiatePrefabForComponent<PlayerInput>(playerPrefab, startPoint.position,
                    Quaternion.identity, null);
            Container
                .Bind<PlayerInput>()
                .FromInstance(playerInstance)
                .AsSingle();
        }
    }
}
