using TheFinalPassword.Models;

namespace TheFinalPassword.Managers;

public class MissionManager
{
    private List<Mission> missions = new List<Mission>
    {
        new Mission("find the clue", new List<string> { "Open note1.txt" }, "+1 reputation")
    };

    private int currentIndex = 0;
    public Mission CurrentMission => missions[currentIndex];

    public string GetDisplayText()
    {
        if (CurrentMission.Completed)
        {
            return $"Mission complete: {CurrentMission.Title}\nReward: {CurrentMission.Reward}";
        }

        return $"Mission: {CurrentMission.Title}\nObjectives: {string.Join(", ", CurrentMission.Objectives)}";
    }

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
