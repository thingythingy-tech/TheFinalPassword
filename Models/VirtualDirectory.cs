namespace TheFinalPassword.Models;

public class VirtualDirectory
{
    public string Name { get; set;  }
    public List<VirtualFile> Files { get; set; } = new List<VirtualFile>();
    public List<VirtualDirectory> SubDirectories { get; set; } = new List<VirtualDirectory>();
    public VirtualDirectory? Parent { get; set; }

    public VirtualDirectory(string name, VirtualDirectory? parent = null)
    {
        Name = name;
        Parent = parent;
    }
}
