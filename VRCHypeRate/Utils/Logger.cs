namespace VRCHypeRate.Utils;

public class Logger
{
    public static Logger GetLogger(string className)
    {
        return new Logger(className);
    }

    private readonly string ClassName;

    private Logger(string className)
    {
        ClassName = className;
    }

    public void Log(string message)
    {
        var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        var messages = message.Split("\n");
        foreach (var msg in messages)
        {
            Console.WriteLine($"[{time}] [{ClassName}]: {msg}");
        }
    }
}