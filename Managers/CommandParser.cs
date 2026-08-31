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

    public string ProcessCommand(string rawInput)
    {
        if (string.IsNullOrWhiteSpace(rawInput))
            return "Please enter a command.";

        var parts = rawInput.Trim().Split(' ', 2);
        string command = parts[0].ToLower();
        string argument = parts.Length > 1 ? parts[1] : "";

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
                SaveData? saveData = saveManager.LoadGame();
                if (saveData == null) return "No save found.";
                player.Name = saveData.PlayerName;
                player.Inventory = saveData.Inventory;
                player.Progress = saveData.Progress;
                player.Reputation = saveData.Reputation;
                networkManager.AuthenicationStatus = saveData.AuthenicationStatus;
                return "Game loaded successfully.";
            }
            default:
                return $"Unknown command: '{command}'. Type 'help' for a list of commands.";
        }
    }
}
