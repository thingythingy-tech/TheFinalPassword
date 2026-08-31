using TheFinalPassword.Models;

namespace TheFinalPassword.Managers;
using System.Text.Json;

public class SaveManager
{
    private string savePath = "savegame.json";

    public void SaveGame(SaveData data)
    {
        string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true});
        File.WriteAllText(savePath, json);
    }
    public SaveData LoadGame()
    {
        if (!SaveExists()) return null;
        string json = File.ReadAllText(savePath);
        return JsonSerializer.Deserialize<SaveData>(json);
    }
}