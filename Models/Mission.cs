namespace TheFinalPassword.Models;

public class Mission
{
    public string Title { get; set; }
    public List<string> Objectives { get; set; }
    public bool Completed { get; set; }
    public string Reward { get; set; }

    public Mission(string title, List<string> objectives, string reward)
    {
        Title = title;
        Objectives = objectives;
        Reward = reward;
    }
}