using UnityEngine;
using Zenject;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

public class LoggerManager : ILogger, IDisposable
{
    private string _logFile;
    private StreamWriter _writer;
    private DateTime _dateNow;
    private readonly object _lock = new object();
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
        lock (_lock)
        {
            _writer = new StreamWriter(_logFile, true);
        }
    }
    private void SaveLogFile()
    {
        lock (_lock)
        {
            if (_writer != null)
            {
                _writer.Close();
                _writer.Dispose();
            }
        }
    }
    public void Log(string message)
    {
        
        WriteToFile(message);
    }
    private void WriteToFile(string message)
    {
        Task.Run(() =>
        {
            lock (_lock)
            {
                try
                {
                    _writer.WriteLine($"{_dateNow} - {message}");
                    _writer.WriteLine($"Current Thread ID: {Thread.CurrentThread.ManagedThreadId}");
                    _writer.Flush();
                }
                catch (IOException e)
                {
                    Debug.LogError($"WriteToFile() can't worked: {e.Message}");
                }
            }
        });
    }
    public void LogErrors(string message)
    {
        WriteToFile("ERROR - " + message);
    }
    
    public void Dispose()
    {
        SaveLogFile();
        _writer?.Dispose();
    }


}
