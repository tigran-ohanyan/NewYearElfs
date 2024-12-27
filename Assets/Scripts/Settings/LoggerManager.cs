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
   /* private CancellationTokenSource _cancellationTokenSource;
    private Task _loggingTask; */
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

        //LoggerInOtherThread();
    }
    /* TO DELETE 
    private void LoggerInOtherThread()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        var token = _cancellationTokenSource.Token;

        _loggingTask = Task.Run(async () =>
        {
            while (!token.IsCancellationRequested)
            {
                await Task.Delay(1000);
            }
        }, token);
    } */

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
       /* _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
        _loggingTask?.Dispose(); */
    }


}
