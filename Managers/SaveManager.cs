using TheFinalPassword.Models;

namespace TheFinalPassword.Managers;
using System.Text.Json;

public class SaveManager
{
    private string savePath = "savegame.json";
    // Serializes the current game state and writes it to the save file.
    public void SaveGame(SaveData data)
    {
        string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(savePath, json);
    }
    // Reads the save file and turns it back into game state if a save exists.
    public SaveData? LoadGame()
    {
        if (!SaveExists()) return null;
        string json = File.ReadAllText(savePath);
        return JsonSerializer.Deserialize<SaveData>(json);
    }
    // Checks whether there is a save file to load.
    private bool SaveExists()
    {
        return File.Exists(savePath);
    }
}
