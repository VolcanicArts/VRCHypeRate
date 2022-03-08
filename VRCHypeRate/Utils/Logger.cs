namespace VRCHypeRate.Utils;

public class Logger
{
    public static LogLevel LogLevel = LogLevel.Verbose;
    public static Logger GetLogger(string className)
    {
        return new Logger(className);
    }

    private readonly string ClassName;

    private Logger(string className)
    {
        ClassName = className;
    }

    public void Log(string message, LogLevel logLevel = LogLevel.Verbose)
    {
        if (logLevel < LogLevel) return;
        var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        var messages = message.Split("\n");
        foreach (var msg in messages)
        {
            Console.WriteLine($"[{time}] [{logLevel}] [{ClassName}]: {msg}");
        }
    }
}

public enum LogLevel
{
    Debug = 0,
    Verbose = 1
}