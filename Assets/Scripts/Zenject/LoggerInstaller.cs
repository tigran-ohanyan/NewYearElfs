using Zenject;

public class LoggerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ILogger>().To<LoggerManager>().AsSingle();
    }
}