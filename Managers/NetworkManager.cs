using System.Windows.Forms.VisualStyles;
using TheFinalPassword.Models;

namespace TheFinalPassword.Managers;

public class NetworkManager
{
    public Server ConnectedServer = null!;
    public bool AuthenicationStatus;

    private List<Server> knownServers = new List<Server>
    {
        new Server("mainframe", "SPACEBAR123",true)
    };

    public string ConnectServer(string name)
    {
        var server = knownServers.FirstOrDefault( s => s.Name == name);
        if (server == null) return $"Server '{name}' not found.";
        ConnectedServer = server;
        AuthenicationStatus = !server.RequiresAuth;
        return AuthenicationStatus 
            ? $"Connected to {name}." 
            : $"Connected to {name}. Authentication required - use 'login<password>' .";
        
    }

    public string Authenicate(string attemptedPassword)
    {
        if (ConnectedServer == null) return "Not connected to any server.";
        if (attemptedPassword == ConnectedServer.Password)
        {
            AuthenicationStatus = true;
            return "Access granted.";
        }
        return "Incorrect password. Access denied";
    }

}