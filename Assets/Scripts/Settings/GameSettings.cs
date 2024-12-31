using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Zenject;

public class GameSettings : MonoBehaviour
{
	private ISave _save;
	[Inject]
	public void Construct(ISave _ISave){
		_save = _ISave;
		GetSave();
        
    }

	private async void GetSave()
    {
        await _save.LoadPlayerDataAsync();
        
        _save.PlayerData.PlayerLevel = 5;

        Debug.Log($"Уровень игрока: {_save.PlayerData.PlayerLevel}");
        Debug.Log($"Здоровье игрока: {_save.PlayerData.Health}");
        Debug.Log($"Очки игрока: {_save.PlayerData.Score}");
    }

    

    [SerializeField] private Sprite _enabledSounds, _disabledSounds;
    private IAudio _audioSettings;
    [Inject]
    public void Construct(IAudio _IAudio)
    {
        _audioSettings = _IAudio;
    }
    
    public void InteractAudioButton(string groupParameter){	
        if(_audioSettings.GetGroupStatus(groupParameter) == true){
            _audioSettings.DisableGroup(groupParameter);
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite = _disabledSounds;
        }else{
            _audioSettings.EnableGroup(groupParameter);
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite = _enabledSounds;
        }
    }
	
	
    private ICache _cache;

    [Inject]
    public void Construct(ICache _ICache)
    {
        _cache = _ICache;
    }
    
    private ILogger _logger;

    [Inject]
    public void Construct(ILogger _ILogger)
    {
        _logger = _ILogger;
    }
    
    
}
