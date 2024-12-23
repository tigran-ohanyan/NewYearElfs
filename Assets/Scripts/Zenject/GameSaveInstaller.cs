using Zenject;
public class GameSaveInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ISave>().To<GameSaveManager>().AsSingle().NonLazy();
    }
}
