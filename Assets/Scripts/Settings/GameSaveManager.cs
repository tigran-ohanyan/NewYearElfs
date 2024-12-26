using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;
using Zenject;
public class GameSaveManager : ISave, IDisposable
{
    private string _saveFile;
    public PlayerData _playerData { get; private set; }
    public PlayerData PlayerData
    {
        get => _playerData;
        set
        {
            _playerData = value;
           Task.Run(async () => await SavePlayerData());
        }
    }

    private StreamWriter _writer;
    private StreamReader _reader;
    
    [Inject]
    public void Construct()
    {
        _saveFile = Path.Combine(Application.persistentDataPath, "save.json");
    }
    
    private ILogger _logger;

    [Inject]
    public void Construct(ILogger _ILogger)
    {
        _logger = _ILogger;
    }
    public async Task SavePlayerData()
    {
        try
        {
            string json = JsonConvert.SerializeObject(PlayerData, Formatting.Indented);
			_logger.Log($"JsonWriter = {json}");

            using (_writer = new StreamWriter(_saveFile, false))
       		{
            	await _writer.WriteLineAsync(json);
            	await _writer.FlushAsync();
      		}
            Debug.Log("Player Succssfully saved!");
        }
        catch (Exception e)
        {
            _logger.LogErrors($"Player can't saved - {e.Message}");
        }
    }
    
    public async Task LoadPlayerDataAsync()
    {
        try
        {
            if (File.Exists(_saveFile))
            {
                using (_reader = new StreamReader(_saveFile))
                {
                    string json = await _reader.ReadToEndAsync();
                    PlayerData = JsonConvert.DeserializeObject<PlayerData>(json);
                }

                Debug.Log("Save loaded");
            }
            else
            {
                Debug.Log("Save.json file doesn't exist! Creating new file.");
                PlayerData = new PlayerData();
            }
        }
        catch (Exception e)
        {
            _logger.LogErrors($"Can't read save file : {e.Message}");
            PlayerData = new PlayerData();
        }
    }
    public void Dispose()
    {
        _writer?.Dispose();
        _reader?.Dispose();
    }
}
