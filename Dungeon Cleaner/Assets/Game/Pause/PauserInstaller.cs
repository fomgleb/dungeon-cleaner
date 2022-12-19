using Zenject;

namespace Game.Pause
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
