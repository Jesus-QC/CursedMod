// -----------------------------------------------------------------------
// <copyright file="CursedRound.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using GameCore;
using RoundRestarting;

namespace CursedMod.Features.Wrappers.Round;

public static class CursedRound
{
    public static bool IsInLobby => !HasStarted && !HasEnded;
    
    public static bool HasStarted => RoundStart.RoundStarted;

    public static bool HasEnded => RoundSummary.singleton._roundEnded;
    
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

    public static int RoundKills
    {
        get => RoundSummary.Kills;
        set => RoundSummary.Kills = value;
    }

    public static int SurvivedScps
    {
        get => RoundSummary.SurvivingSCPs;
        set => RoundSummary.SurvivingSCPs = value;
    }

    public static int UpTime => RoundRestart.UptimeRounds;

    public static TimeSpan RoundTime => RoundStart.RoundLength;

    public static short LobbyTimer
    {
        get => RoundStart.singleton.Timer;
        set => RoundStart.singleton.NetworkTimer = value;
    }
    
    public static void ForceStart() => PluginAPI.Core.Round.Start();
    
    public static void ForceEnd() => PluginAPI.Core.Round.End();
    
    public static void ForceRestart(bool fastRestart) => PluginAPI.Core.Round.Restart(fastRestart);
    
    public static void RestartSilently() => PluginAPI.Core.Round.RestartSilently();
}