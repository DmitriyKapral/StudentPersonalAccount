namespace StudentPersonalAccount.Logging;

/// <summary>
/// 
/// </summary>
public class Logger
{
    private static Logger _instance;
    private static readonly object lockObject = new object();
    private Logger()
    {
    }

    public static Logger Instance 
    { 
        get 
        {
            if (_instance == null)
            {
                lock (lockObject)
                {
                    _instance ??= new Logger();
                }
            }
            return _instance;
        } 
    }

    /// <summary>
    /// Метод вывода логов
    /// </summary>
    /// <param name="message"></param>
    public void Log(string message)
    {
        // В данном примере просто выводим сообщение в консоль
        Console.WriteLine($"Logging: {DateTime.Now}: {message}");
    }

}
