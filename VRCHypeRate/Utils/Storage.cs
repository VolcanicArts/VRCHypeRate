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
}