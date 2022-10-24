using Zenject;

namespace UnnamedGame.Pause
{
    public class PauserInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<Pauser>()
                .FromNew()
                .AsSingle();
        }
    }
}
