namespace TheFinalPassword.Models;

public class SaveData
{
    public string PlayerName { get; set; }
    public List<string> Inventory { get; set; }
    public int Progress { get; set; }
    public int Reputation { get; set; }
    public bool AuthenicationStatus { get; set; }
}