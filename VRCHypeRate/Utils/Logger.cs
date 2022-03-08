namespace VRCHypeRate.Utils;

public class Logger
{
    private const LogLevel LogLevel = Utils.LogLevel.Verbose;
    public const string LogFilePath = "./runtimelog.txt";

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
        logToFile(message, LogLevel.Error);
        logToConsole(message, LogLevel.Error);
    }

    public void Log(string message, LogLevel logLevel = LogLevel.Verbose)
    {
        logToFile(message, logLevel);
        if (logLevel < LogLevel) return;
        logToConsole(message, logLevel);
    }

    private void logToConsole(string message, LogLevel logLevel)
    {
        createFormattedLogMessages(message, logLevel).ForEach(Console.WriteLine);
        if (logLevel == LogLevel.Error) Console.ReadLine();
    }

    private void logToFile(string message, LogLevel logLevel)
    {
        Storage.CreateOrAppendFile(LogFilePath, createFormattedLogMessages(message, logLevel));
    }

    private List<string> createFormattedLogMessages(string message, LogLevel logLevel)
    {
        var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        return message.Split("\n").Select(msg => $"[{time}] [{logLevel}] [{ClassName}]: {msg}").ToList();
    }
}

public enum LogLevel
{
    Error = -1,
    Debug = 0,
    Verbose = 1
}