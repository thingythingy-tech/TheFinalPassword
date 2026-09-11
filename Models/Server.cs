namespace TheFinalPassword.Models;

// Defines a server that the player can connect to.
public class Server
{
    public string Name { get; set; }
    public string Password { get; set; }
    public bool RequiresAuth { get; set; }
    // Creates a server with its login requirements.
    public Server(string name, string password, bool requiresAuth)
    {
        Name = name;
        Password = password;
        RequiresAuth = requiresAuth;
    }
}