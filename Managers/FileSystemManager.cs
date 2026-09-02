using TheFinalPassword.Models;

namespace TheFinalPassword.Managers;

public class FileSystemManager
{
    private readonly Player player;
    private readonly Dictionary<string, string> collectibleFiles = new()
    {
        { "note1.txt", "Mainframe Prefix Clue" },
        { "recovery.txt", "Recovery Word Clue" },
        { "combine.txt",  "Password Combination Clue"}
    };

    public VirtualDirectory Root;
    public VirtualDirectory CurrentDirectory;

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

    public string ListContents()
    {
        var items = new List<string>();
        items.AddRange(CurrentDirectory.SubDirectories.Select(d => "[DIR]" + d.Name));
        items.AddRange(CurrentDirectory.Files.Select(f => f.Name));
        return items.Count == 0 ? "(empty)" : string.Join("\n", items);
    }

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

        var target = CurrentDirectory.SubDirectories.FirstOrDefault(d => d.Name == name);
        if (target == null) return $"Directory '{name}' not found.";
        CurrentDirectory = target;
        return "Moved to" + CurrentDirectory.Name;
    }

    public string OpenFile(string name)
    {
        var file = CurrentDirectory.Files.FirstOrDefault(f => f.Name == name);
        if (file == null) return $"File '{name}' not found.";

        if (collectibleFiles.TryGetValue(file.Name, out string? itemName) && !player.Inventory.Any(item => item.StartsWith(itemName)))
        {
            player.AddItem($"{itemName}: {file.Content}");
            return file.Content + $"\n\nAdded to inventory: {itemName}";
        }

        return file.Content;
    }
}

