namespace TheFinalPassword.Models;

// Represents a single mission, including its objectives, reward, and completion state.
public class Mission
{
    public string Title { get; set; }
    public List<string> Objectives { get; set; }
    public bool Completed { get; set; }
    public string Reward { get; set; }
    // Creates a mission with the text the player will see and the reward earned for completing it.
    public Mission(string title, List<string> objectives, string reward)
    {
        Title = title;
        Objectives = objectives;
        Reward = reward;
    }
}