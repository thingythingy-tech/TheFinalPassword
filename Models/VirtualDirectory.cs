namespace TheFinalPassword.Models;

// Represents a folder in the game's in-memory virtual filesystem.
public class VirtualDirectory
{
    public string Name { get; set;  }
    public List<VirtualFile> Files { get; set; } = new List<VirtualFile>();
    public List<VirtualDirectory> SubDirectories { get; set; } = new List<VirtualDirectory>();
    public VirtualDirectory? Parent { get; set; }
    // Creates a virtual directory and optionally links it back to its parent.
    public VirtualDirectory(string name, VirtualDirectory? parent = null)
    {
        Name = name;
        Parent = parent;
    }
}
