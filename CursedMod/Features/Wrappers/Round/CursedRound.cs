using System;
using GameCore;
using RoundRestarting;

namespace CursedMod.Features.Wrappers.Round;

public static class CursedRound
{
    public static void ForceStart() => PluginAPI.Core.Round.Start();
    
    public static void ForceEnd() => PluginAPI.Core.Round.End();
    
    public static void ForceRestart(bool fastRestart) => PluginAPI.Core.Round.Restart(fastRestart);
    
    public static void RestartSilently() => PluginAPI.Core.Round.RestartSilently();

    public static bool IsRoundStarted => RoundStart.RoundStarted;

    public static bool IsRoundLocked
    {
        get => PluginAPI.Core.Round.IsLocked;
        set => PluginAPI.Core.Round.IsLocked = value;
    }

    public static bool IsLobbyLocked
    {
        get => RoundStart.LobbyLock;
        set => RoundStart.LobbyLock = value;
    }

    public static int ClassDEscaped
    {
        get => RoundSummary.EscapedClassD;
        set => RoundSummary.EscapedClassD = value;
    }

    public static int ScientistEscaped
    {
        get => RoundSummary.EscapedScientists;
        set => RoundSummary.EscapedScientists = value;
    }

    public static int ZombiesConverted
    {
        get => RoundSummary.ChangedIntoZombies;
        set => RoundSummary.ChangedIntoZombies = value;
    }

    public static int UpTime => RoundRestart.UptimeRounds;

    public static TimeSpan RoundTime => RoundStart.RoundLength;
}