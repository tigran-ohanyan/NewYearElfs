using UnityEngine;
using UnityEngine.Audio;
using Zenject;
public class AudioManager : IAudio
{
    private AudioMixer _audioMixer;

    [Inject]
    public void Construct(AudioMixer audioMixer)
    {
        _audioMixer = audioMixer;
    }

    public bool GetGroupStatus(string groupParameter)
    {
        _audioMixer.GetFloat(groupParameter, out float _volume);
        return _volume == 0f ? true : false;
    }

    public void DisableGroup(string groupParameter)
    {
        _audioMixer.SetFloat(groupParameter, -80f);
        
    }
    public void EnableGroup(string groupParameter)   
    {
        _audioMixer.SetFloat(groupParameter, 0f);
    }

	
}