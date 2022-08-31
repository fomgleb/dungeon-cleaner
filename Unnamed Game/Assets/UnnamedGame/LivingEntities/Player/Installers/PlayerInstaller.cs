using UnityEngine;
using Zenject;

namespace UnnamedGame.LivingEntities.Player.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Transform startPoint;
        
        public override void InstallBindings()
        {
            var playerGameObject =
                Container.InstantiatePrefab(playerPrefab, startPoint.position, Quaternion.identity, null);
            Container
                .Bind<GameObject>()
                .FromInstance(playerGameObject)
                .AsSingle();
        }
    }
}