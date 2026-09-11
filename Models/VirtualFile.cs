namespace TheFinalPassword.Models;

// Represents a readable file inside the game's virtual filesystem.
public class VirtualFile
{
    public string Name { get; set; }
    public string Content { get; set; }
    // Creates a virtual file with the text that should appear when it is opened.
    public VirtualFile(string name, string content)
    {
        Name = name;
        Content = content;
    }
}