using System.Text;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;

public class FileWriter
{
    private string _folder;
    private string _filePath;
    private FileAppender _appender;
    private Thread _workingThread;
    private readonly ConcurrentQueue<LogMessage> _messages = new ConcurrentQueue<LogMessage>();
    private bool _disposing;

    private const string DATE_FORMAT = "yyyy-MM-dd";
    private const string LogTimeFormat = "{0:dd/MM/yyyy HH:mm:ss:ffff} [{1}]: {2}\r";
    public FileWriter(string folder)
    {
        _folder = folder;
        ManagePath();
        _workingThread = new Thread(StoreMessages)
        {
            IsBackground = true,
            Priority = ThreadPriority.BelowNormal
        };

    }

    private void ManagePath()
    {
        _filePath = $"{_folder}/{DateTime.UtcNow.ToString(DATE_FORMAT)}.log";
    }
    public void Write(LogMessage message)
    {
        
        _messages.Enqueue(message);
    }
    private void StoreMessages()
    {
        while (!_disposing)
        {
            while (!_messages.IsEmpty)
            {
                try
                {
                    LogMessage message;
                    if (!_messages.TryPeek(out message)) // берем message, но не удаляем его из коллекции
                    {
                        Thread.Sleep(5);
                    }

                    if (_appender == null || _appender.FileName != _filePath) // на случай, если поменялся путь
                    {
                        _appender = new FileAppender(_filePath); // пересоздаём
                    }
                    var messageToWrite = string.Format(LogTimeFormat, message.Time, message.Type, message.Message);
                    if (_appender.Append(messageToWrite))
                    {
                        _messages.TryDequeue(out message);
                    }
                    else
                    {
                        Thread.Sleep(5);
                    }
                }
                catch (Exception e)
                {
                    break;
                }
            }
        }
    }
}
