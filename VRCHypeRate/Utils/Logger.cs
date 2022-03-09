using System.Diagnostics;

namespace VRCHypeRate.Utils;

public static class Logger
{
    private const LogLevel LogLevel = Utils.LogLevel.Verbose;
    public const string LogFilePath = "./runtimelog.txt";

    public static void Error(string message)
    {
        Log(message, LogLevel.Error);
    }

    public static void Log(string message, LogLevel logLevel = LogLevel.Verbose)
    {
        var className = getClassName() ?? "Unknown";
        var logMessages = createFormattedLogMessages(message, className, logLevel);

        Storage.CreateOrAppendFile(LogFilePath, logMessages);
        if (logLevel < LogLevel) return;
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
        return message.Split("\n").Select(msg => $"[{time}] [{logLevel}] [{className}]: {msg}").ToList();
    }
}

public enum LogLevel
{
    Debug = 0,
    Verbose = 1,
    Error = 2
}