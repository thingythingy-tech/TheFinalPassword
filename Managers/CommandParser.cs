namespace TheFinalPassword.Managers;

public class CommandParser
{
    private readonly FileSystemManager fileSystemManager;
    private readonly Player player;

    public CommandParser(FileSystemManager fsm, Player player)
    {
        fileSystemManager = fsm;
        this.player = player;
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
                return "Commands: help, look, cd, open, inventory";
            case "look":
            case "ls":
                return fileSystemManager.ListContents();
            case "cd":
                return fileSystemManager.ChangeDirectory(argument);
            case "open":
                return fileSystemManager.OpenFile(argument);
            case "inventory":
                return player.DisplayInventory();
            default:
                return $"Unknown command: '{command}'. Type 'help' for a list of commands.";
        }
    }
}
