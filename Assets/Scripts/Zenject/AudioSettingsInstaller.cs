using Zenject;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSettingsInstaller : MonoInstaller
{
    [SerializeField] private AudioMixer _audioMixer;

    public override void InstallBindings()
    {
        Container.Bind<AudioMixer>().FromInstance(_audioMixer).AsSingle();
        Container.Bind<IAudio>().To<AudioManager>().AsSingle();
    }
}
