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
    public PlayerData PlayerData { get; private set; }
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
    public void SavePlayerData()
    {
        try
        {
            string json = JsonConvert.SerializeObject(PlayerData, Formatting.Indented);
            using (_writer = new StreamWriter(_saveFile, false))
            {
                _writer.WriteLine(json);
                _writer.Flush();
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

                Debug.Log("Данные игрока загружены.");
            }
            else
            {
                Debug.Log("Файл данных не найден. Создаётся новый.");
                PlayerData = new PlayerData();
            }
        }
        catch (Exception e)
        {
            _logger.LogErrors($"Can't read save file : {e.Message}");
            PlayerData = new PlayerData();
        }
    }

    /*public async Task OnApplicationQuitAsync()
    {
        await SavePlayerDataAsync();
        Dispose();
    }*/
    public void Dispose()
    {
        _writer?.Dispose();
        _reader?.Dispose();
    }
    
#if UNITY_EDITOR
     private void OnApplicationQuit()
     {
Debug.Log("Application Quit");
         SavePlayerData();
         Dispose();
     }
#else
    private void OnApplicationPause()
    {Debug.Log("Application Quit");

        SavePlayerData();
        Dispose();
    }
#endif

}
