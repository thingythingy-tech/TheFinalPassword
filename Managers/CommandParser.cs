namespace TheFinalPassword.Managers;
using TheFinalPassword.Models;

public class CommandParser
{
    private readonly FileSystemManager fileSystemManager;
    private readonly Player player;
    private readonly NetworkManager networkManager;
    private readonly SaveManager saveManager;

    public CommandParser(FileSystemManager fsm, Player player, NetworkManager networkManager)
    {
        fileSystemManager = fsm;
        this.player = player;
        this.networkManager = networkManager;
        saveManager = new SaveManager();
    }

    // Processes raw text entered by the player by separating the command from
    // its argument and selecting the appropriate action using a switch statement.
    public string ProcessCommand(string rawInput)
    {
        // Reject empty input so that the parser does not attempt to process
        // an invalid command.
        if (string.IsNullOrWhiteSpace(rawInput))
            return "Please enter a command.";

        // Split the input into a command and an optional argument.
        // Limiting the split to two parts means arguments containing spaces are preserved.
        var parts = rawInput.Trim().Split(' ', 2);
        string command = parts[0].ToLower();
        string argument = parts.Length > 1 ? parts[1] : "";

        // A switch statement is used to efficiently match the player's command
        // to the correct manager method.
        switch (command)
        {
            case "help":
                return "Commands: help, look, cd, open, inventory, connect, login";
            case "look":
            case "ls":
                return fileSystemManager.ListContents();
            case "cd":
                return fileSystemManager.ChangeDirectory(argument);
            case "open":
                return fileSystemManager.OpenFile(argument);
            case "inventory":
                return player.DisplayInventory();
            case"connect":
                return networkManager.ConnectServer(argument);
            case "login":
                return networkManager.Authenicate(argument);
            case "save":
            {
                // Create a SaveData object containing the current game state so that
                // the player's progress can be restored when the game is loaded.
                SaveData saveData = new SaveData
                {
                    PlayerName = player.Name,
                    Inventory = player.Inventory,
                    Progress = player.Progress,
                    Reputation = player.Reputation,
                    AuthenicationStatus = networkManager.AuthenicationStatus
                };
                saveManager.SaveGame(saveData);
                return "Game saved successfully.";
            }
            case "load":
            {
                // Load the saved game state and copy the stored values back into the
                // Player and NetworkManager objects.
                SaveData? saveData = saveManager.LoadGame();
                if (saveData == null) return "No save found.";
                player.Name = saveData.PlayerName;
                player.Inventory = saveData.Inventory;
                player.Progress = saveData.Progress;
                player.Reputation = saveData.Reputation;
                networkManager.AuthenicationStatus = saveData.AuthenicationStatus;
                return "Game loaded successfully.";
            }
            // Any command that does not match a recognised case is handled here,
            // preventing invalid input from crashing the program.
            default:
                return $"Unknown command: '{command}'. Type 'help' for a list of commands.";
        }
    }
}
