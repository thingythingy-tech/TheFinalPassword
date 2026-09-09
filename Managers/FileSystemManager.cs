using TheFinalPassword.Models;

namespace TheFinalPassword.Managers;

public class FileSystemManager
{
    private readonly Player player;

    // Stores the files that provide clues to the player and links each filename
    // to the item name that will be added to the player's inventory.
    private readonly Dictionary<string, string> collectibleFiles = new()
    {
        { "note1.txt", "Mainframe Prefix Clue" },
        { "recovery.txt", "Recovery Word Clue" },
        { "combine.txt",  "Password Combination Clue"}
    };

    // Root represents the starting directory while CurrentDirectory tracks
    // the player's current position in the virtual filesystem.
    public VirtualDirectory Root;
    public VirtualDirectory CurrentDirectory;

    // Build the virtual filesystem used by the game.
    // This allows the player to explore files and directories without accessing
    // the actual computer filesystem.
    public FileSystemManager(Player player)
    {
        this.player = player;

        Root = new VirtualDirectory("root");
        var docs = new VirtualDirectory("documents", Root);
        docs.Files.Add(new VirtualFile("note1.txt", "The password to the mainframe starts with 'SPACE'."));
        docs.Files.Add(new VirtualFile("recovery.txt", "Old recovery logs indicate the mainframe password suffix is 'BAR123'."));
        docs.Files.Add(new VirtualFile("combine.txt", "The two password clues must be combined to recover the mainframe password."));
        Root.SubDirectories.Add(docs);
        CurrentDirectory = Root;
    }

    // Build a list containing both directories and files in the current directory
    // before displaying them to the player.
    public string ListContents()
    {
        var items = new List<string>();
        items.AddRange(CurrentDirectory.SubDirectories.Select(d => "[DIR]" + d.Name));
        items.AddRange(CurrentDirectory.Files.Select(f => f.Name));
        return items.Count == 0 ? "(empty)" : string.Join("\n", items);
    }

    // Changes the player's current location within the virtual filesystem.
    // The parent reference allows the player to move back towards the root.
    public string ChangeDirectory(string name)
    {
        if (name == ". .")
        {
            if (CurrentDirectory.Parent == null)
            {
                return "Already at root.";
            }

            CurrentDirectory = CurrentDirectory.Parent;
            return "Moved to" + CurrentDirectory.Name;
        }

        // Search the current directory for a subdirectory matching the player's input.
        // FirstOrDefault returns null if no matching directory exists.
        var target = CurrentDirectory.SubDirectories.FirstOrDefault(d => d.Name == name);
        if (target == null) return $"Directory '{name}' not found.";
        CurrentDirectory = target;
        return "Moved to" + CurrentDirectory.Name;
    }

    // Find the requested file in the current directory and return its contents.
    // If the file contains a collectible clue, it is also added to the inventory.
    public string OpenFile(string name)
    {
        // Search for the requested file within the current directory.
        var file = CurrentDirectory.Files.FirstOrDefault(f => f.Name == name);
        if (file == null) return $"File '{name}' not found.";

        // Check whether the opened file is a collectible clue and only add it
        // to the inventory if it has not already been collected.
        if (collectibleFiles.TryGetValue(file.Name, out string? itemName) && !player.Inventory.Any(item => item.StartsWith(itemName)))
        {
            player.AddItem($"{itemName}: {file.Content}");
            return file.Content + $"\n\nAdded to inventory: {itemName}";
        }

        return file.Content;
    }
}
