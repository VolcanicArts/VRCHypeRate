using Newtonsoft.Json;

namespace VRCHypeRate.Utils;

public static class Storage
{
    public static T GetFileAsJson<T>(string relativeFilePath)
    {
        if (!File.Exists(relativeFilePath)) throw new FileNotFoundException($"Could not find specified file {relativeFilePath}");
        var fileContents = File.ReadAllText(relativeFilePath);
        var jsonContents = JsonConvert.DeserializeObject<T>(fileContents);
        if (jsonContents == null) throw new JsonException($"Could not deserialize the file contents into {nameof(T)}");
        return jsonContents;
    }

    public static void DeleteFile(string relativeFilePath)
    {
        File.Delete(relativeFilePath);
    }

    public static void CreateOrAppendFile(string relativeFilePath, List<string> lines)
    {
        lines.ForEach(line => CreateOrAppendFile(relativeFilePath, line));
    }

    public static void CreateOrAppendFile(string relativeFilePath, string line)
    {
        if (!File.Exists(relativeFilePath)) File.Create(relativeFilePath).Close();
        using var sw = File.AppendText(relativeFilePath);
        sw.WriteLine(line);
    }
}