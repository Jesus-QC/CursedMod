// -----------------------------------------------------------------------
// <copyright file="CursedRoundEventsHandler.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace CursedMod.Events.Handlers;

public static class CursedRoundEventsHandler
{
    public static event CursedEventManager.CursedEventHandler RoundStarted;
    
    public static event CursedEventManager.CursedEventHandler WaitingForPlayers;
    
    public static event CursedEventManager.CursedEventHandler RestartingRound;
    
    public static event CursedEventManager.CursedEventHandler RoundEnded;

    internal static void OnRoundStarted()
    {
        RoundStarted.InvokeEvent();
    }

    internal static void OnWaitingForPlayers()
    {
        WaitingForPlayers.InvokeEvent();
    }

    internal static void OnRestartingRound()
    {
        RestartingRound.InvokeEvent();
    }

    internal static void OnRoundEnded()
    {
        RoundEnded.InvokeEvent();
    }
}