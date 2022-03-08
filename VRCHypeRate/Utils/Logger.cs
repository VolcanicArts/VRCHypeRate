namespace VRCHypeRate.Utils;

public class Logger
{
    private const LogLevel LogLevel = Utils.LogLevel.Verbose;

    public static Logger GetLogger(string className)
    {
        return new Logger(className);
    }

    private readonly string ClassName;

    private Logger(string className)
    {
        ClassName = className;
    }

    public void Error(string message)
    {
        log(message, LogLevel.Error);
    }

    public void Log(string message, LogLevel logLevel = LogLevel.Verbose)
    {
        if (logLevel < LogLevel) return;
        log(message, logLevel);
    }

    private void log(string message, LogLevel logLevel)
    {
        var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        var messages = message.Split("\n");
        foreach (var msg in messages)
        {
            Console.WriteLine($"[{time}] [{logLevel}] [{ClassName}]: {msg}");
        }

        if (logLevel == LogLevel.Error) Console.ReadLine();
    }
}

public enum LogLevel
{
    Error = -1,
    Debug = 0,
    Verbose = 1
}