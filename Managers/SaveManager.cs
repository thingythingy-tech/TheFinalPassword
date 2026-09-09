using TheFinalPassword.Models;

namespace TheFinalPassword.Managers;
using System.Text.Json;

public class SaveManager
{
    // Defines the filename used to store the player's saved game state.
    private string savePath = "savegame.json";

    // Convert the SaveData object into JSON so the current game state can be
    // stored permanently in a file.
    public void SaveGame(SaveData data)
    {
        // Write the JSON with indentation so that the saved data is easier to inspect
        // and understand during development.
        string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(savePath, json);
    }

    // Check whether a save file exists before attempting to read it.
    // The JSON is then converted back into a SaveData object.
    public SaveData? LoadGame()
    {
        if (!SaveExists()) return null;
        string json = File.ReadAllText(savePath);
        return JsonSerializer.Deserialize<SaveData>(json);
    }

    // Prevent an error when loading by checking that the save file actually exists.
    private bool SaveExists()
    {
        return File.Exists(savePath);
    }
}
