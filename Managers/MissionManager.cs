using TheFinalPassword.Models;

namespace TheFinalPassword.Managers;

public class MissionManager
{
    // Missions are kept in a list so they are displayed in the correct order.
    private List<Mission> missions = new List<Mission>
    {
        new Mission("find the clue", new List<string> { "Open note1.txt" }, "+1 reputation")
    };

    private int currentIndex = 0;
    public Mission CurrentMission => missions[currentIndex];
    // Formats the current mission so it can be shown in the game window.
    public string GetDisplayText()
    {
        if (CurrentMission.Completed)
        {
            return $"Mission complete: {CurrentMission.Title} ✓\nReward: {CurrentMission.Reward}";
        }

        return $"Mission: {CurrentMission.Title}\nObjectives: {string.Join(", ", CurrentMission.Objectives)}";
    }
    // Marks the active mission as complete and moves to the next mission if one exists.
    public string CompleteCurrentMission()
    {
        if (CurrentMission.Completed)
        {
            return "Mission already completed.";
        }

        CurrentMission.Completed = true;
        string reward = CurrentMission.Reward;
        if (currentIndex < missions.Count - 1) currentIndex++;
        return $"Mission completed! Reward: {reward}";
    }
}
