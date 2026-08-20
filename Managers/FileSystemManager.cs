using TheFinalPassword.Models;

namespace TheFinalPassword.Managers;

public class FileSystemManager
{
    public VirtualDirectory Root;
    public VirtualDirectory CurrentDirectory;

    public FileSystemManager()
    {
        Root = new VirtualDirectory("root");
        var docs = new VirtualDirectory("documents", Root);
        docs.Files.Add(new VirtualFile("note1.txt", " The password to the mainframe starts with 'SPACE'.'"));
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
        return file == null ? $"File '{name}' not found." : file.Content;
    }
}