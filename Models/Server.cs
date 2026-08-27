namespace TheFinalPassword.Models;

public class Server
{
    public string Name { get; set; }
    public string Password { get; set; }
    public bool RequiresAuth { get; set; }

    public Server(string name, string password, bool requiresAuth)
    {
        Name = name;
        Password = password;
        RequiresAuth = requiresAuth;
    }
}