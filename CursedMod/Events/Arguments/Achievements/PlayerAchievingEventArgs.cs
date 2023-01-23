using System;
using Achievements;
using CursedMod.Features.Wrappers.Player;
using Mirror;

namespace CursedMod.Events.Arguments.Achievements;

public class PlayerAchievingEventArgs : EventArgs, ICursedCancellableEvent, ICursedPlayerEvent
{
    public bool IsAllowed { get; set; }

    public CursedPlayer Player { get; set; }
    
    public AchievementName Achievement { get; set; }

    public PlayerAchievingEventArgs(NetworkConnection connection, AchievementName achievement)
    {
        IsAllowed = true;
        Player = CursedPlayer.Get(connection.identity);
        Achievement = achievement;
    }
}