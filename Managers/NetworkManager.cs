using System.Windows.Forms.VisualStyles;
using TheFinalPassword.Models;

namespace TheFinalPassword.Managers;

public class NetworkManager
{
    public Server ConnectedServer = null!;
    public bool AuthenicationStatus;

    // Stores the servers available in the game and the information required
    // to determine whether authentication is needed.
    private List<Server> knownServers = new List<Server>
    {
        new Server("mainframe", "SPACEBAR123",true)
    };

    public string ConnectServer(string name)
    {
        // Search the list of known servers for one with the name entered by the player.
        var server = knownServers.FirstOrDefault( s => s.Name == name);
        if (server == null) return $"Server '{name}' not found.";
        ConnectedServer = server;

        // The authentication requirement determines whether the player receives
        // immediate access or must provide a password.
        AuthenicationStatus = !server.RequiresAuth;
        return AuthenicationStatus 
            ? $"Connected to {name}." 
            : $"Connected to {name}. Authentication required - use 'login <password>' .";
        
    }

    public string Authenicate(string attemptedPassword)
    {
        if (ConnectedServer == null) return "Not connected to any server.";

        // Compare the entered password with the password stored for the connected server.
        if (attemptedPassword == ConnectedServer.Password)
        {
            AuthenicationStatus = true;
            return "Access granted.";
        }
        return "Incorrect password. Access denied";
    }

}
