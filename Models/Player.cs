// Stores the player's current state, including inventory, progress, and reputation.
public class Player
{
    public string Name { get; set; } = "Player";
    public List<string> Inventory { get; set; } = new List<string>();
    public int Progress { get; set; }
    public int Reputation { get; set; }

    // These keep common player state changes in one place.
    public void AddItem(string item) => Inventory.Add(item);
    public void RemoveItem(string item) => Inventory.Remove(item);
    public void UpdateProgress(int amount) => Progress += amount;
    // displays the Inventory in the terminal.
    public string DisplayInventory()
    {
        return Inventory.Count == 0 ? "Inventory empty." : string.Join(", ", Inventory);
    }
}