namespace TheFinalPassword.Managers;

public class CommandParser
{
    private readonly FileSystemManager fileSystemManager;

    public CommandParser(FileSystemManager fsm)
    {
        fileSystemManager = fsm;
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
                return "Commands: help, look, cd, open";
            case "look":
            case "ls":
                return fileSystemManager.ListContents();
            case "cd":
                return fileSystemManager.ChangeDirectory(argument);
            case "open":
                return fileSystemManager.OpenFile(argument);
            default:
                return $"Unknown command: '{command}'. Type 'help' for a list of commands.";
        }
    }
}
