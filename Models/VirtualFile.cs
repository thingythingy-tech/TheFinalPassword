namespace TheFinalPassword.Models;

public class VirtualFile
{
    public string Name { get; set; }
    public string Content { get; set; }
    public VirtualFile(string name, string content)
    {
        Name = name;
        Content = content;
    }
}