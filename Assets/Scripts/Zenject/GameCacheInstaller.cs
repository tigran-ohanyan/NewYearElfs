using Zenject;

public class GameCacheInstaller : MonoInstaller
{
   public override void InstallBindings()
   {
      Container.Bind<ICache>().To<GameCacheManager>().AsSingle();
   }
}
