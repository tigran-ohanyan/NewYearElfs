using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SceneLoaderInstaller : MonoInstaller
{
    [SerializeField] private Animator loadingAnimator;

    public override void InstallBindings()
    {
        Container.Bind<Animator>().FromInstance(loadingAnimator).AsSingle();
        Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
    }
}