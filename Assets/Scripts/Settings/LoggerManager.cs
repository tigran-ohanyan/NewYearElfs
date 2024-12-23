using UnityEngine;
using Zenject;
using System;
using System.IO;
using System.Threading.Tasks;

public class LoggerManager : ILogger
{
    private string _logFile;
    private StreamWriter writer;
    private DateTime _dateNow;
    [Inject]
    public void Construct()
    {
        _dateNow = DateTime.Now;
        OpenLogFile();
    }

    private void OpenLogFile()
    {
        _logFile = Path.Combine(Application.persistentDataPath, "log.txt");
        Debug.Log(Path.Combine(Application.persistentDataPath, "log.txt"));
    }

    private void SaveLogFile()
    {
        writer.Close();
    }
    public void Log(string message)
    {
        WriteToFile(message).ContinueWith(_ => OpenLogFile());
    }
    private async Task WriteToFile(string text)
    {
        try
        {
            using (writer = new StreamWriter(_logFile, true))
            {
                await writer.WriteLineAsync(_dateNow + " - " + text);
                writer.Flush();
            }
        }
        catch (IOException e)
        {
            Debug.LogError($"Ошибка записи: {e.Message}");
        }
    }
    public void LogErrors(string message)
    {
        WriteToFile("ERROR - " + message).ContinueWith(_ => OpenLogFile());
    }
#if UNITY_EDITOR
     void OnApplicationQuit()
     {
         SaveLogFile();
     }
#else
    void OnApplicationPause()
    {
        SaveLogFile();
    }
#endif

}
