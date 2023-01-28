using CursedMod.Events.Arguments.Achievements;

namespace CursedMod.Events.Handlers.Achievements;

public static class AchievementsEventHandler
{
    public static event EventManager.CursedEventHandler<PlayerAchievingEventArgs> PlayerAchieving;

    public static void OnPlayerAchieving(PlayerAchievingEventArgs args)
    {
        PlayerAchieving.InvokeEvent(args);
    }
}