public class Player
{
    public string Name { get; set; } = "Player";
    public List<string> Inventory { get; set; } = new List<string>();
    public int Progress { get; set; }
    public int Reputation { get; set; }

    public void AddItem(string item) => Inventory.Add(item);
    public void RemoveItem(string item) => Inventory.Remove(item);
    public void UpdateProgress(int amount) => Progress += amount;

    public string DisplayInventory()
    {
        return Inventory.Count == 0 ? "Inventory empty." : string.Join(", ", Inventory);
    }
}