using Newtonsoft.Json;

namespace VRCHypeRate.Utils;

public static class Storage
{
    public static T GetFileAsJson<T>(string relativeFilePath)
    {
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
        using var sw = File.AppendText(relativeFilePath); 
        lines.ForEach(line => sw.WriteLine(line));
    }
}