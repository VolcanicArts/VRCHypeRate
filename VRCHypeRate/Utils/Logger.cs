using System.Diagnostics;

namespace VRCHypeRate.Utils;

public static class Logger
{
    private const LogLevel DefaultLogLevel = 0;
    public const string LogFilePath = "./runtimelog.txt";

    public static void Error(string message)
    {
        Log(message, LogLevel.Error);
    }

    public static void Log(string message, LogLevel logLevel = DefaultLogLevel)
    {
        var className = getClassName() ?? "Unknown";
        var logMessages = createFormattedLogMessages(message, className, logLevel);

        Storage.CreateOrAppendFile(LogFilePath, logMessages);
        if (logLevel < DefaultLogLevel) return;
        logMessages.ForEach(Console.WriteLine);
        if (logLevel == LogLevel.Error) Console.ReadLine();
    }

    private static string? getClassName()
    {
        return new StackTrace().GetFrame(2)?.GetMethod()?.ReflectedType?.Name;
    }

    private static List<string> createFormattedLogMessages(string message, string? className, LogLevel logLevel)
    {
        var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        var maxLength = Enum.GetValues(typeof(LogLevel)).Cast<LogLevel>().Select(logLevelName => logLevelName.ToString().Length).Prepend(0).Max();
        return message.Split("\n").Select(msg => $"[{time}] [{logLevel.ToString().PadRight(maxLength)}] [{className}]: {msg}").ToList();
    }
}

public enum LogLevel
{
    Debug = -1,
    Verbose = 0,
    Error = 1
}